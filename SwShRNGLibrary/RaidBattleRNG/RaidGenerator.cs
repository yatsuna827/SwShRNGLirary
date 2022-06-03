using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;
using PokemonStandardLibrary;
using PokemonStandardLibrary.CommonExtension;
using PokemonStandardLibrary.Gen8;

namespace SwShRNGLibrary
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
            var poke = slot.pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if (slot.isShinyLocked && !slot.ForceShiny) isShily = isSquare = false;

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

            uint Ability = slot.ForceHiddenAbility ? 2 : (slot.allowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            Nature nature = poke.Name == "ストリンダー" ? (poke.FormName == "ハイ" || poke.FormName=="ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);

            return slot.pokemon.GetIndividual(Lv, IVs, EC, PID, nature, Ability, gender).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public Pokemon.Individual Generate(RaidBattleSlot slot, Criteria criteria, uint Lv = 60)
        {
            var rng = (seed, FIXEDSEED);
            var poke = slot.pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if ((!criteria.allowShiny || slot.isShinyLocked) && !slot.ForceShiny) isShily = isSquare = false;
            if (!criteria.CheckShiny(isShily, isSquare)) return Pokemon.Individual.Empty;

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

            if (!criteria.CheckIVs(IVs)) return Pokemon.Individual.Empty;

            uint Ability = slot.ForceHiddenAbility ? 2 : (slot.allowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            if (!criteria.CheckAbility(poke.Ability[Ability])) return Pokemon.Individual.Empty;

            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            if (!criteria.CheckGender(gender)) return Pokemon.Individual.Empty;

            Nature nature = poke.Name == "ストリンダー" ? (poke.FormName == "ハイ" || poke.FormName == "ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);
            if (!criteria.CheckNature(nature)) return Pokemon.Individual.Empty;

            return slot.pokemon.GetIndividual(Lv, IVs, EC, PID, nature, Ability, gender).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public static Pokemon.Individual Generate(RaidBattleSlot slot, (ulong s0, ulong s1) rng, uint Lv = 60)
        {
            var poke = slot.pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if (slot.isShinyLocked && !slot.ForceShiny) isShily = isSquare = false;

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

            uint Ability = slot.ForceHiddenAbility ? 2 : (slot.allowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253) + 1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            Nature nature = poke.Name == "ストリンダー" ? (poke.FormName == "ハイ" || poke.FormName == "ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);

            return slot.pokemon.GetIndividual(Lv, IVs, EC, PID, nature, Ability, gender).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public static Pokemon.Individual Generate(RaidBattleSlot slot, (ulong s0, ulong s1) rng, Criteria criteria, uint Lv = 60)
        {
            var poke = slot.pokemon;

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = slot.ForceShiny || ShinyValue < 16;
            bool isSquare = (slot.ForceShiny && ShinyValue >= 16) || ShinyValue == 0;
            if ((!criteria.allowShiny || slot.isShinyLocked) && !slot.ForceShiny) isShily = isSquare = false;
            if (!criteria.CheckShiny(isShily, isSquare)) return Pokemon.Individual.Empty;

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

            if (!criteria.CheckIVs(IVs)) return Pokemon.Individual.Empty;

            uint Ability = slot.ForceHiddenAbility ? 2 : (slot.allowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2));
            if (!criteria.CheckAbility(poke.Ability[Ability])) return Pokemon.Individual.Empty;

            Gender gender = slot.FixedGender != Gender.Genderless || poke.GenderRatio == GenderRatio.Genderless ? slot.FixedGender : (((uint)rng.GetRand(253)+1) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            if (!criteria.CheckGender(gender)) return Pokemon.Individual.Empty;

            Nature nature = poke.Name == "ストリンダー" ? (poke.FormName == "ハイ" || poke.FormName == "ハイキョダイ" ? HighNature[rng.GetRand(13)] : LowNature[rng.GetRand(12)]) : (Nature)rng.GetRand(25);
            if (!criteria.CheckNature(nature)) return Pokemon.Individual.Empty;

            return slot.pokemon.GetIndividual(Lv, IVs, EC, PID, nature, Ability, gender).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public void Advance(uint num = 1) { seed += FIXEDSEED * num; }
        public void Back(uint num = 1) { seed -= FIXEDSEED * num; }
        public RaidGenerator(ulong iniSeed)
        {
            seed = iniSeed;
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

        public bool CheckShiny(bool isShiny, bool isSquare) => !checkShiny || checkSquare && isSquare || checkStar && isShiny && !isSquare;
        public bool CheckIVs(uint[] IVs) => !checkIV || IVs.Select((iv, i) => (iv, i)).All(_ => !checkIVs[_.i] || MinIVs[_.i] <= _.iv && _.iv <= MaxIVs[_.i]);
        public bool CheckAbility(string ability) => !checkAbility || ability == targetAbility;
        public bool CheckGender(Gender gender) => !checkGender || gender == targetGender;
        public bool CheckNature(Nature nature) => !checkNature || nature == targetNature;

        public Criteria() { MinIVs = new uint[6]; MaxIVs = new uint[6]; checkIVs = new bool[6]; }
    }
}
