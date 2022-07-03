using System;
using System.Collections.Generic;
using System.Linq;
using PokemonPRNG.Xoroshiro128p;
using PokemonStandardLibrary;
using PokemonStandardLibrary.CommonExtension;
using PokemonStandardLibrary.Gen8;
using SwShRNGLibrary.EncounterData;

namespace SwShRNGLibrary.Overworld
{
    class RandomEncounterSlot
    {
        public Pokemon.Species Species { get; }
        public uint BasicLv { get; }
        public uint VariableLv { get; }
        public uint MaxLv { get => BasicLv + VariableLv - 1; }

        public HoldingItem HoldingItems { get => Species.HoldingItems(); }

        public RandomEncounterSlot(Pokemon.Species species, uint basicLv, uint variableLv)
        {
            Species = species;
            BasicLv = basicLv;
            VariableLv = variableLv;
        }
    }
    class SlotSelector : ISideEffectiveGeneratable<RandomEncounterSlot>
    {
        private readonly IEnumerable<(uint Rate, RandomEncounterSlot Slot)> _table;
        public RandomEncounterSlot Generate(ref (ulong s0, ulong s1) seed)
        {
            var rand = seed.GetRand(100);
            foreach (var (rate, slot) in _table)
            {
                if (rand < rate) return slot;
            }

            throw new Exception("ここには来ないはず");
        }

        public SlotSelector(RandomEncounterTable table)
        {
            var sum = 0u;
            _table = table.Table.Select(_ => (sum += _.Rate, new RandomEncounterSlot(_.Pokemon, table.BasicLv, table.VariableLv)));

            if (sum != 100) throw new Exception($"出現率の合計が100%になっていません; {sum}");
        }
    }


    class RandomSymbolEncounterGenerator
    {
        private readonly IndividualGenerator _indivGenerator;

        private readonly SlotSelector _slotSelector;
        private readonly LvGenerator _lvGenerator;
        private readonly ShinyRollGenerator _pidGenerator;
        private readonly ISideEffectiveGeneratable<Mark> _markGenerator;
        private readonly ISideEffectiveGeneratable<string, HoldingItem> _itemSelector;

        // 倒した数で変動
        private readonly uint _auraThreshold = 3;
        private readonly uint _auraShinyRollBonus = 1;

        public void Generate((ulong s0, ulong s1) seed)
        {
            seed.Advance();

            seed.GetRand(100); // unknown

            seed.GetRand(100); // fieldAbility

            var slot = seed.Generate(_slotSelector);
            var lv = seed.Generate(_lvGenerator, slot);

            // ここで生成された証は破棄される
            seed.Generate(_markGenerator);

            var aura = seed.GetRand(1000) < _auraThreshold; // オーラ持ち
            if (aura) lv = slot.MaxLv; // オーラ持ちになったら Lv = Max で固定される

            var isShiny = seed.Generate(_pidGenerator, aura ? _auraShinyRollBonus : 0);

            // メロボ判定通っている場合はスキップ
            var gender = seed.GetRand(2) == 0 ? Gender.Female : Gender.Male;
            var nature = (Nature)seed.GetRand(25);
            var ability = slot.Species.Ability[(int)seed.GetRand(2)];

            // 持ち物持っている可能性があるときだけ
            // 固定シンボルの場合も判定は無い
            var item = seed.Generate(_itemSelector, slot.HoldingItems);

            var ivsBonus = aura ? 2 + (uint)seed.GetRand(2) : 0;
            // オーラ持ちかつ遺伝技がある場合, GetRand(#eggMoves)で遺伝技
            var eggMoves = (uint)slot.Species.EggMoves().Length;
            if (aura && eggMoves > 0) seed.GetRand(eggMoves);

            _indivGenerator.Generate((uint)seed.GetRand(), isShiny, ivsBonus);

            var mark = seed.Generate(_markGenerator);
        }
    }

    class HiddenEncounterGenerator
    {
        private readonly IndividualGenerator _indivGenerator;

        private readonly SlotSelector _slotSelector;
        private readonly LvGenerator _lvGenerator;
        private readonly ShinyRollGenerator _pidGenerator;
        private readonly ISideEffectiveGeneratable<Mark> _markGenerator;
        private readonly ISideEffectiveGeneratable<string, HoldingItem> _itemSelector;

        public void Generate((ulong s0, ulong s1) seed)
        {
            seed.Advance();

            seed.GetRand(100); // unknown

            seed.GetRand(100); // fieldAbility

            var slot = seed.Generate(_slotSelector);
            var lv = seed.Generate(_lvGenerator, slot);

            seed.Generate(_markGenerator); // ここで生成されたのは破棄される

            var isShiny = seed.Generate(_pidGenerator);

            // メロボ判定通っている場合はスキップ
            var gender = seed.GetRand(2) == 0 ? Gender.Female : Gender.Male;
            var nature = (Nature)seed.GetRand(25);
            var ability = slot.Species.Ability[(int)seed.GetRand(2)];

            var item = seed.Generate(_itemSelector, slot.HoldingItems);

            _indivGenerator.Generate((uint)seed.GetRand(), isShiny, 0);

            var mark = seed.Generate(_markGenerator);
        }
    }

    class FishingEncounterGenerator
    {
        private readonly IndividualGenerator _indivGenerator;

        private readonly SlotSelector _slotSelector;
        private readonly LvGenerator _lvGenerator;
        private readonly ShinyRollGenerator _pidGenerator;
        private readonly ISideEffectiveGeneratable<Mark> _markGenerator;
        private readonly ISideEffectiveGeneratable<string, HoldingItem> _itemSelector;

        // 倒した数で変動
        private readonly uint _auraShinyRollBonus = 1;
        private readonly bool _aura;


        public void Generate((ulong s0, ulong s1) seed)
        {
            seed.GetRand(100); // unknown

            seed.GetRand(100); // fieldAbility

            var slot = seed.Generate(_slotSelector);
            var lv = seed.Generate(_lvGenerator, slot);
            if (_aura) lv = slot.MaxLv;

            // ここで生成された証は破棄される
            seed.Generate(_markGenerator);

            // PIDの仮生成
            var isShiny = seed.Generate(_pidGenerator, _aura ? _auraShinyRollBonus : 0);

            // メロボ判定通っている場合はスキップ
            var gender = seed.GetRand(2) == 0 ? Gender.Female : Gender.Male;
            var nature = (Nature)seed.GetRand(25);
            var ability = slot.Species.Ability[(int)seed.GetRand(2)];

            var item = seed.Generate(_itemSelector, slot.HoldingItems);

            var ivsBonus = _aura ? 2 + (uint)seed.GetRand(2) : 0;
            var eggMoves = (uint)slot.Species.EggMoves().Length;
            if (_aura && eggMoves > 0) seed.GetRand(eggMoves);

            _indivGenerator.Generate((uint)seed.GetRand(), isShiny, ivsBonus);

            var mark = seed.Generate(_markGenerator);
        }
    }

    public class StaticSymbolGenerator : IGeneratable<(Pokemon.Individual Individual, Mark Mark)>
    {
        private readonly StaticEncounterData _slot;
        private readonly ShinyRollGenerator _pidGenerator;
        private readonly IndividualGenerator _indivGenerator;
        private readonly ISideEffectiveGeneratable<Mark> _markGenerator;

        public (Pokemon.Individual Individual, Mark Mark) Generate((ulong s0, ulong s1) seed)
        {
            seed.GetRand(100);

            var isShiny = seed.Generate(_pidGenerator);

            // メロボ判定通っている場合はスキップ
            var gender = seed.GetRand(2) == 0 ? Gender.Female : Gender.Male;
            if (_slot.Pokemon.GenderRatio.IsFixed()) gender = _slot.Pokemon.GenderRatio.ToFixedGender().Value;

            var nature = (Nature)seed.GetRand(25);
            var ability = _slot.FixedAbility ?? (uint)seed.GetRand(2);

            var (ec, pid, ivs, shiny) = _indivGenerator.Generate((uint)seed.GetRand(), !_slot.ShinyLocked && isShiny, _slot.FlawlessIVs);

            var mark = seed.Generate(_markGenerator);

            return (_slot.Pokemon.GetIndividual(_slot.Lv, ivs, ec, pid, nature, ability, gender).SetShinyType(shiny), mark);
        }

        public StaticSymbolGenerator(uint tsv, StaticEncounterData slot, Weather weather = Weather.Normal, Time time = Time.Others, bool hasShinyCharm = false, bool hasMarkCharm = false)
        {
            _slot = slot;
            _pidGenerator = new ShinyRollGenerator(tsv, hasShinyCharm ? 3u : 1);
            _indivGenerator = new IndividualGenerator(tsv);

            var mark = new MarkGenerator(weather, time);
            _markGenerator = hasMarkCharm ? new RerollMarkGenerator(mark, 3) : mark as ISideEffectiveGeneratable<Mark>;
        }
    }



    public class IndividualGenerator
    {
        private readonly uint _tsv;
        public (uint EC, uint PID, uint[] IVs, ShinyType Shiny) Generate(uint initialSeed, bool isShiny, uint flawlessIVs)
        {
            var seed = ((ulong)initialSeed, Xoroshiro128p.FIXSEED);

            var ec = (uint)seed.GetRand();
            var pid = (uint)seed.GetRand();

            if (!isShiny && pid.IsShiny(_tsv))
                pid ^= 0x10000000; // Antishiny
            else if (isShiny && !pid.IsShiny(_tsv))
                pid = (_tsv ^ pid) << 16 | pid & 0xFFFF;

            var ivs = seed.GenerateIVs(flawlessIVs);

            return (ec, pid, ivs, pid.ToShinyValue().ToShinyType(_tsv));
        }

        public IndividualGenerator(uint tsv)
        {
            _tsv = tsv;
        }
    }

    class LvGenerator : ISideEffectiveGeneratable<uint, RandomEncounterSlot>
    {
        public uint Generate(ref (ulong s0, ulong s1) seed, RandomEncounterSlot slot)
        {
            if (slot.VariableLv <= 1) return slot.BasicLv;

            return slot.BasicLv + (uint)seed.GetRand(slot.VariableLv);
        }
    }
    class ShinyRollGenerator : ISideEffectiveGeneratable<bool>, ISideEffectiveGeneratable<bool, uint>
    {
        // おまもり持ちなら3回
        private readonly uint _reroll;
        private readonly uint _tsv;
        public bool Generate(ref (ulong s0, ulong s1) seed)
        {
            for (int i = 0; i < _reroll; i++)
            {
                if (((uint)seed.GetRand()).IsShiny(_tsv)) return true;
            }

            return false;
        }
        public bool Generate(ref (ulong s0, ulong s1) seed, uint bonus)
        {
            for (int i = 0; i < _reroll + bonus; i++)
            {
                if (((uint)seed.GetRand()).IsShiny(_tsv)) return true;
            }

            return false;
        }

        public ShinyRollGenerator(uint tsv)
        {
            _reroll = 1;
            _tsv = tsv;
        }
        public ShinyRollGenerator(uint tsv, uint reroll)
        {
            _reroll = reroll;
            _tsv = tsv;
        }
    }

    public class MarkGenerator : IGeneratable<Mark>, ISideEffectiveGeneratable<Mark>
    {
        private readonly Mark _weather;
        private readonly Mark _time;
        private readonly bool _fishing;

        public Mark Generate((ulong s0, ulong s1) seed) => Generate(ref seed);

        public Mark Generate(ref (ulong s0, ulong s1) seed)
        {
            var rare = seed.GetRand(1000) == 0;
            var personality = seed.GetRand(100) == 0;
            var uncommon = seed.GetRand(50) == 0;
            var weather = seed.GetRand(50) == 0;
            var time = seed.GetRand(50) == 0;
            var fishing = seed.GetRand(25) == 0;

            if (rare) return Mark.Rare;
            if (personality) return Mark.Personality.Marks[(int)seed.GetRand(28)];
            if (uncommon) return Mark.Uncommon;
            if (weather && _weather != null) return _weather;
            if (time && _time != null) return _time;
            if (fishing && _fishing) return Mark.Fishing;

            return Mark.Null;
        }

        public MarkGenerator(Weather weather = Weather.Normal, Time time = Time.Others, bool fishing = false)
        {
            _weather = weather != 0 ? Mark.Weather.Marks[(int)weather] : null;
            _time = time != 0 ? Mark.Time.Marks[(int)time - 1] : null;
            _fishing = fishing;
        }
    }
    public class RerollMarkGenerator : IGeneratable<Mark>, ISideEffectiveGeneratable<Mark>
    {
        private readonly int _rolls;
        private readonly ISideEffectiveGeneratable<Mark> _generator;

        public Mark Generate((ulong s0, ulong s1) seed) => Generate(ref seed);
        public Mark Generate(ref (ulong s0, ulong s1) seed)
        {
            for (int i = 0; i < _rolls; i++)
            {
                var mark = seed.Generate(_generator);
                if (mark != Mark.Null) return mark;
            }

            return Mark.Null;
        }

        public RerollMarkGenerator(ISideEffectiveGeneratable<Mark> generator, int rolls)
        {
            _rolls = rolls;
            _generator = generator;
        }
    }

    public class HoldingItemSelector : ISideEffectiveGeneratable<string, HoldingItem>
    {
        public string Generate(ref (ulong s0, ulong s1) seed, HoldingItem holdingItem)
        {
            if (holdingItem is null) return "";
            if (holdingItem.Guaranteed != null) return holdingItem.Guaranteed;

            // ここの対応関係違うかもしれない
            var roll = seed.GetRand(100);
            if (roll < 45) return "";
            if (roll < 95) return holdingItem.Common ?? "";
            return holdingItem.Rare ?? "";
        }
    }
    public class CompoundEyeHoldingItemSelector : ISideEffectiveGeneratable<string, HoldingItem>
    {
        public string Generate(ref (ulong s0, ulong s1) seed, HoldingItem holdingItem)
        {
            if (holdingItem is null) return "";
            if (holdingItem.Guaranteed != null) return holdingItem.Guaranteed;

            // ここの対応関係違うかもしれない
            var roll = seed.GetRand(100);
            if (roll < 20) return "";
            if (roll < 80) return holdingItem.Common ?? "";
            return holdingItem.Rare ?? "";
        }
    }

    static class GenerationExt
    {
        public static uint ToShinyValue(this uint val)
            => val & 0xFFFF ^ val >> 16;

        public static ShinyType ToShinyType(this uint psv, uint tsv)
        {
            var sv = psv ^ tsv;
            if (sv >= 16) return ShinyType.NotShiny;

            return sv == 0 ? ShinyType.Square : ShinyType.Star;
        }

        public static bool IsShiny(this uint pid, uint tsv)
            => (pid.ToShinyValue() ^ tsv) < 16;

        public static uint[] GenerateIVs(ref this (ulong s0, ulong s1) seed, uint flawlessIVs)
        {
            var ivs = new[] { 32u, 32u, 32u, 32u, 32u, 32u };
            for (int i = 0; i < flawlessIVs; i++)
            {
                while (true)
                {
                    var idx = seed.GetRand(6);
                    if (ivs[idx] == 32)
                    {
                        ivs[idx] = 31;
                        break;
                    }
                }
            }
            for (int i = 0; i < 6; i++)
                if (ivs[i] == 32) ivs[i] = (uint)seed.GetRand(32);

            return ivs;
        }

        public static Gender? ToFixedGender(this GenderRatio ratio)
        {
            switch (ratio)
            {
                case GenderRatio.Genderless: return Gender.Genderless;
                case GenderRatio.FemaleOnly: return Gender.Female;
                case GenderRatio.MaleOnly: return Gender.Male;
                default: return null;
            }
        }

        public static Gender GenerateGender(ref this (ulong s0, ulong s1) seed, GenderRatio ratio)
            => ratio.ToFixedGender() ?? (seed.GetRand(253) + 1 < (uint)ratio ? Gender.Female : Gender.Male);

        public static Gender GenerateGender(ref this (ulong s0, ulong s1) seed, GenderRatio ratio, Gender cuteCharmGender)
            => ratio.ToFixedGender() ??
                (cuteCharmGender != Gender.Genderless && seed.GetRand(3) != 0 ? cuteCharmGender :
                seed.GetRand(253) + 1 < (uint)ratio ? Gender.Female : Gender.Male);

        private static (uint, uint) GenerateBrilliantInfo(uint KOs)
        {
            if (KOs >= 500) return (30, 6);
            if (KOs >= 300) return (30, 5);
            if (KOs >= 200) return (30, 4);
            if (KOs >= 100) return (30, 3);
            if (KOs >= 50) return (25, 2);
            if (KOs >= 20) return (20, 1);
            if (KOs >= 1) return (15, 1);

            return (0, 0);
        }
    }
}
