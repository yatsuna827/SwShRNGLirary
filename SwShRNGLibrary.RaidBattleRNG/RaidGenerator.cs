using System;
using System.Linq;
using PokemonPRNG.Xoroshiro128p;
using PokemonStandardLibrary;
using PokemonStandardLibrary.Gen8;
using SwShRNGLibrary.RaidBattleData;

namespace SwShRNGLibrary.RaidBattleRNG
{
    public class RaidGenerator
    {
        public ulong seed { get; private set; }
        private const ulong FIXEDSEED = 0x82A2B175229D6A5B;
        private static readonly Nature[] HighNature = new Nature[] { Nature.Adamant, Nature.Naughty, Nature.Brave, Nature.Impish, Nature.Lax, Nature.Rash, Nature.Sassy, Nature.Hasty, Nature.Jolly, Nature.Naive, Nature.Hardy, Nature.Docile, Nature.Quirky };
        private static readonly Nature[] LowNature = new Nature[] { Nature.Lonely, Nature.Bold, Nature.Relaxed, Nature.Timid, Nature.Serious, Nature.Modest, Nature.Mild, Nature.Quiet, Nature.Bashful, Nature.Calm, Nature.Gentle, Nature.Careful };
        public Pokemon.Individual Generate(RaidBattleSlot slot, uint Lv = 60)
        {
            var rng = (seed, FIXEDSEED);
            var poke = slot.Pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if (slot.IsShinyLocked && !slot.ForceShiny) isShily = isSquare = false;

            uint[] IVs = new uint[6];
            int i = 0;
            while (i < slot.FlawlessIVs)
            {
                uint stat = (uint)rng.GetRand(6);
                if (IVs[stat] == 31) continue;
                IVs[stat] = 31;
                i++;
            }
            for (int k = 0; k < 6; k++)
                if (IVs[k] == 0) IVs[k] = (uint)(rng.GetRand() & 0x1f);

            uint Ability = slot.ForceHiddenAbility ? 2 : (slot.AllowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            Nature nature = poke.Name == "ストリンダー" ? (poke.Form == "ハイ" || poke.Form=="ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);

            var height = (byte)(rng.GetRand(129) + rng.GetRand(128));
            var weight = (byte)(rng.GetRand(129) + rng.GetRand(128));

            return slot.Pokemon.GetIndividual(Lv, IVs, EC, PID, nature, Ability, gender, height, weight).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public Pokemon.Individual Generate(RaidBattleSlot slot, Criteria criteria, uint Lv = 60)
        {
            var rng = (seed, FIXEDSEED);
            var poke = slot.Pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if ((!criteria.allowShiny || slot.IsShinyLocked) && !slot.ForceShiny) isShily = isSquare = false;
            if (!criteria.CheckShiny(isShily, isSquare)) return null;

            uint[] IVs = new uint[6];
            int i = 0;
            while (i < slot.FlawlessIVs)
            {
                uint stat = (uint)rng.GetRand(6);
                if (IVs[stat] == 31) continue;
                IVs[stat] = 31;
                i++;
            }
            for (int k = 0; k < 6; k++)
                if (IVs[k] == 0) IVs[k] = (uint)(rng.GetRand() & 0x1f);

            if (!criteria.CheckIVs(IVs)) return null;

            int Ability = slot.ForceHiddenAbility ? 2 : (slot.AllowHiddenAbility ? (int)rng.GetRand(3) : (int)rng.GetRand(2));
            if (!criteria.CheckAbility(poke.Ability[Ability])) return null;

            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            if (!criteria.CheckGender(gender)) return null;

            Nature nature = poke.Name == "ストリンダー" ? (poke.Form == "ハイ" || poke.Form == "ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);
            if (!criteria.CheckNature(nature)) return null;

            var height = (byte)(rng.GetRand(129) + rng.GetRand(128));
            if (!criteria.CheckSize(height)) return null;
            var weight = (byte)(rng.GetRand(129) + rng.GetRand(128));

            return slot.Pokemon.GetIndividual(Lv, IVs, EC, PID, nature, (uint)Ability, gender, height, weight).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public static Pokemon.Individual Generate(RaidBattleSlot slot, (ulong s0, ulong s1) rng, uint Lv = 60)
        {
            var poke = slot.Pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if (slot.IsShinyLocked && !slot.ForceShiny) isShily = isSquare = false;

            uint[] IVs = new uint[6];
            int i = 0;
            while (i < slot.FlawlessIVs)
            {
                uint stat = (uint)rng.GetRand(6);
                if (IVs[stat] == 31) continue;
                IVs[stat] = 31;
                i++;
            }
            for (int k = 0; k < 6; k++)
                if (IVs[k] == 0) IVs[k] = (uint)(rng.GetRand() & 0x1f);

            uint Ability = slot.ForceHiddenAbility ? 2 : (slot.AllowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            Nature nature = poke.Name == "ストリンダー" ? (poke.Form == "ハイ" || poke.Form == "ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);

            var height = (byte)(rng.GetRand(129) + rng.GetRand(128));
            var weight = (byte)(rng.GetRand(129) + rng.GetRand(128));

            return slot.Pokemon.GetIndividual(Lv, IVs, EC, PID, nature, Ability, gender, height, weight).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public static Pokemon.Individual Generate(RaidBattleSlot slot, (ulong s0, ulong s1) rng, Criteria criteria, uint Lv = 60)
        {
            var poke = slot.Pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if ((!criteria.allowShiny || slot.IsShinyLocked) && !slot.ForceShiny) isShily = isSquare = false;
            if (!criteria.CheckShiny(isShily, isSquare)) return null;

            uint[] IVs = new uint[6];
            int i = 0;
            while (i < slot.FlawlessIVs)
            {
                uint stat = (uint)rng.GetRand(6);
                if (IVs[stat] == 31) continue;
                IVs[stat] = 31;
                i++;
            }
            for (int k = 0; k < 6; k++)
                if (IVs[k] == 0) IVs[k] = (uint)(rng.GetRand() & 0x1f);

            if (!criteria.CheckIVs(IVs)) return null;

            int Ability = slot.ForceHiddenAbility ? 2 : (slot.AllowHiddenAbility ? (int)rng.GetRand(3) : (int)rng.GetRand(2));
            if (!criteria.CheckAbility(poke.Ability[Ability])) return null;

            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253)+1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            if (!criteria.CheckGender(gender)) return null;

            Nature nature = poke.Name == "ストリンダー" ? (poke.Form == "ハイ" || poke.Form == "ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);
            if (!criteria.CheckNature(nature)) return null;

            var height = (byte)(rng.GetRand(129) + rng.GetRand(128));
            if (!criteria.CheckSize(height)) return null;
            var weight = (byte)(rng.GetRand(129) + rng.GetRand(128));

            return slot.Pokemon.GetIndividual(Lv, IVs, EC, PID, nature, (uint)Ability, gender, height, weight).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public void Advance(uint num = 1) { seed += FIXEDSEED * num; }
        public void Back(uint num = 1) { seed -= FIXEDSEED * num; }
        public RaidGenerator(ulong iniSeed)
        {
            seed = iniSeed;
        }
    }

    public sealed class RaidBattleGenerator : IGeneratable<Pokemon.Individual>, IGeneratable<Pokemon.Individual, Criteria>
    {
        private static readonly Nature[] _highNature = new Nature[] { Nature.Adamant, Nature.Naughty, Nature.Brave, Nature.Impish, Nature.Lax, Nature.Rash, Nature.Sassy, Nature.Hasty, Nature.Jolly, Nature.Naive, Nature.Hardy, Nature.Docile, Nature.Quirky };
        private static readonly Nature[] _lowNature = new Nature[] { Nature.Lonely, Nature.Bold, Nature.Relaxed, Nature.Timid, Nature.Serious, Nature.Modest, Nature.Mild, Nature.Quiet, Nature.Bashful, Nature.Calm, Nature.Gentle, Nature.Careful };

        private readonly RaidBattleSlot _slot;
        private readonly uint _lv;

        public Pokemon.Individual Generate((ulong s0, ulong s1) rng)
        {
            var poke = _slot.Pokemon;

            var ec = (uint)(rng.GetRand() & 0xFFFFFFFF);
            var dummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            var pid = (uint)(rng.GetRand() & 0xFFFFFFFF);
            var shinyValue = (((dummyID ^ pid) >> 16) ^ ((dummyID ^ pid) & 0xFFFF));
            var isShily = _slot.ForceShiny || shinyValue < 16;
            var isSquare = (_slot.ForceShiny && shinyValue >= 16) || shinyValue == 0;
            if (_slot.IsShinyLocked && !_slot.ForceShiny) isShily = isSquare = false;

            var ivs = new uint[6];
            int i = 0;
            while (i < _slot.FlawlessIVs)
            {
                var s = (uint)rng.GetRand(6);
                if (ivs[s] == 31) continue;
                ivs[s] = 31;
                i++;
            }
            for (int k = 0; k < ivs.Length; k++)
                if (ivs[k] == 0) ivs[k] = (uint)(rng.GetRand() & 0x1f);

            var ability = _slot.ForceHiddenAbility 
                ? 2 : 
                (_slot.AllowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            var gender = _slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless 
                ? _slot.FixedGender 
                : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            var nature = poke.Name == "ストリンダー" 
                ? (poke.Form == "ハイ" || poke.Form == "ハイキョダイ" 
                    ? _highNature[rng.GetRand(13)] : _lowNature[rng.GetRand(12)]) 
                : (Nature)rng.GetRand(25);

            var height = (byte)(rng.GetRand(129) + rng.GetRand(128));
            var weight = (byte)(rng.GetRand(129) + rng.GetRand(128));

            return _slot.Pokemon.GetIndividual(_lv, ivs, ec, pid, nature, ability, gender, height, weight).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }

        public Pokemon.Individual Generate((ulong s0, ulong s1) rng, Criteria criteria)
        {
            var poke = _slot.Pokemon;

            var ec = (uint)(rng.GetRand() & 0xFFFFFFFF);
            var dummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            var pid = (uint)(rng.GetRand() & 0xFFFFFFFF);
            var shinyValue = (((dummyID ^ pid) >> 16) ^ ((dummyID ^ pid) & 0xFFFF));
            var isShily = _slot.ForceShiny || shinyValue < 16;
            var isSquare = (_slot.ForceShiny && shinyValue >= 16) || shinyValue == 0;
            if ((!criteria.allowShiny || _slot.IsShinyLocked) && !_slot.ForceShiny) isShily = isSquare = false;

            if (!criteria.CheckShiny(isShily, isSquare)) return null;

            var ivs = new uint[6];
            int i = 0;
            while (i < _slot.FlawlessIVs)
            {
                var s = (uint)rng.GetRand(6);
                if (ivs[s] == 31) continue;
                ivs[s] = 31;
                i++;
            }
            for (int k = 0; k < ivs.Length; k++)
                if (ivs[k] == 0) ivs[k] = (uint)(rng.GetRand() & 0x1f);

            if (!criteria.CheckIVs(ivs)) return null;

            var ability = _slot.ForceHiddenAbility ? 2 : (_slot.AllowHiddenAbility ? (int)rng.GetRand(3) : (int)rng.GetRand(2));
            if (!criteria.CheckAbility(poke.Ability[ability])) return null;

            var gender = _slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless 
                ? _slot.FixedGender 
                : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            if (!criteria.CheckGender(gender)) return null;

            var nature = poke.Name == "ストリンダー" 
                ? (poke.Form == "ハイ" || poke.Form == "ハイキョダイ" 
                    ? _highNature[rng.GetRand(13)] : _lowNature[rng.GetRand(12)]) 
                : (Nature)rng.GetRand(25);
            if (!criteria.CheckNature(nature)) return null;

            var height = (byte)(rng.GetRand(129) + rng.GetRand(128));
            if (!criteria.CheckSize(height)) return null;
            var weight = (byte)(rng.GetRand(129) + rng.GetRand(128));

            return poke.GetIndividual(_lv, ivs, ec, pid, nature, (uint)ability, gender, height, weight).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }


        public RaidBattleGenerator(RaidBattleSlot slot, uint lv)
        {
            _slot = slot;
            _lv = lv;
        }
    }

    public class Criteria
    {
        public bool checkStar;
        public bool checkSquare;
        public bool allowShiny { get; set; } = true;
        bool checkShiny => checkStar || checkSquare;
        bool checkIV => checkIVs.Any();
        public bool[] checkIVs;
        public bool checkNature;
        public bool checkAbility;
        public bool checkGender;

        public uint[] MinIVs;
        public uint[] MaxIVs;
        public string targetAbility;
        public Nature targetNature;
        public Gender targetGender;

        public bool checkJumbo;
        public bool checkMini;
        public bool checkSize => checkJumbo || checkMini;

        public bool CheckShiny(bool isShiny, bool isSquare) => !checkShiny || checkSquare && isSquare || checkStar && isShiny && !isSquare;
        public bool CheckIVs(uint[] IVs) => !checkIV || IVs.Select((iv, i) => (iv, i)).All(_ => !checkIVs[_.i] || MinIVs[_.i] <= _.iv && _.iv <= MaxIVs[_.i]);
        public bool CheckAbility(string ability) => !checkAbility || ability == targetAbility;
        public bool CheckGender(Gender gender) => !checkGender || gender == targetGender;
        public bool CheckNature(Nature nature) => !checkNature || nature == targetNature;
        public bool CheckSize(byte height) => !checkSize || (checkJumbo && height == 0xFF) || (checkMini && height == 0);

        public Criteria() { MinIVs = new uint[6]; MaxIVs = new uint[6]; checkIVs = new bool[6]; }
    }
}
