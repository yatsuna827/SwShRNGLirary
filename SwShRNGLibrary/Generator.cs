using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{

    public class RaidGenerator
    {
        public ulong seed { get; private set; }
        private const ulong FIXEDSEED = 0x82A2B175229D6A5B;
        private Nature[] HighNature = new Nature[] { Nature.Adamant, Nature.Naughty, Nature.Brave, Nature.Impish, Nature.Lax, Nature.Rash, Nature.Sassy, Nature.Hasty, Nature.Jolly, Nature.Naive, Nature.Hardy, Nature.Docile, Nature.Quirky };
        public Pokemon.Individual Generate(int FlawlessIVs, Pokemon.Species poke, bool allowHiddenAbility, Gender FixGender = Gender.Genderless)
        {
            Xoroshiro128p rng = new Xoroshiro128p(seed, FIXEDSEED);

            uint EC = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint DummyID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint PID = (uint)(rng.GetRand() & 0xFFFFFFFF);
            uint ShinyValue = (((DummyID ^ PID) >> 16) ^ ((DummyID ^ PID) & 0xFFFF));
            bool isShily = ShinyValue < 16;
            bool isSquare = ShinyValue == 0;

            uint[] IVs = new uint[6];
            int i = 0;
            while (i < FlawlessIVs)
            {
                uint stat = (uint)rng.GetRand(6);
                if (IVs[stat] == 31) continue;
                IVs[stat] = 31;
                i++;
            }
            for (int k = 0; k < 6; k++)
                if (IVs[k] == 0) IVs[k] = (uint)(rng.GetRand() & 0x1f);

            uint Ability = allowHiddenAbility ? (uint)rng.GetRand(3) : (uint)rng.GetRand(2);
            Gender gender = poke.GenderRatio.isFixed() ? Gender.Genderless : ((uint)rng.GetRand(253) < (uint)poke.GenderRatio ? Gender.Female : Gender.Male);
            Nature nature = poke.FormName == "ハイ" ? HighNature[rng.GetRand(13)] : (Nature)rng.GetRand(25);

            return poke.GetIndividual(60, IVs, EC, PID, nature, Ability, gender).SetShinyType(isShily ? (isSquare ? ShinyType.Square : ShinyType.Star) : ShinyType.NotShiny);
        }
        public void Advance(uint num = 1) { seed += FIXEDSEED * num; }
        public void Back(uint num = 1) { seed -= FIXEDSEED * num; }
        public RaidGenerator(ulong iniSeed)
        {
            seed = iniSeed;
        }
    }
}
