using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public class RaidBattleSlot
    {
        public readonly Pokemon.Species pokemon;
        public readonly bool allowHiddenAbility;
        public readonly int FlawlessIVs;
        public bool ForceHiddenAbility { get; private set; }
        public Gender FixedGender { get; private set; }
        public Nature FixedNature { get; private set; } = Nature.other;
        public bool ForceShiny { get; private set; }
        internal RaidBattleSlot BeForceHiddenAbility() { ForceHiddenAbility = true; return this; }
        internal RaidBattleSlot BeForceShiny() { ForceShiny = true; return this; }
        internal RaidBattleSlot FixGender(Gender gender) { FixedGender = gender; return this; }
        internal RaidBattleSlot FixNature(Nature nature) { FixedNature = nature; return this; }


        internal RaidBattleSlot(string name, bool allowHA, int fix)
        {
            pokemon = Pokemon.GetPokemon(name);
            allowHiddenAbility = allowHA;
            FlawlessIVs = fix;

            switch (pokemon.GenderRatio)
            {
                case GenderRatio.MaleOnly:
                    FixedGender = Gender.Male; break;
                case GenderRatio.FemaleOnly:
                    FixedGender = Gender.Female; break;
                default:
                    FixedGender = Gender.Genderless; break;
            }
        }
        internal RaidBattleSlot(string name, string form, bool allowHA, int fix)
        {
            pokemon = Pokemon.GetPokemon(name, form);
            allowHiddenAbility = allowHA;
            FlawlessIVs = fix;

            switch (pokemon.GenderRatio)
            {
                case GenderRatio.MaleOnly:
                    FixedGender = Gender.Male; break;
                case GenderRatio.FemaleOnly:
                    FixedGender = Gender.Female; break;
                default:
                    FixedGender = Gender.Genderless; break;
            }
        }
    }
    public class CommonSlot1 : RaidBattleSlot
    {
        internal CommonSlot1(string name, Rom appver = Rom.both) : base(name, false, 1) { }
        internal CommonSlot1(string name, string form, Rom appver = Rom.both) : base(name, form, false, 1) { }
    }
    public class CommonSlot2 : RaidBattleSlot
    {
        internal CommonSlot2(string name, Rom appver = Rom.both) : base(name, false, 2) { }
        internal CommonSlot2(string name, string form, Rom appver = Rom.both) : base(name, form, false, 2) { }
    }
    public class CommonSlot3 : RaidBattleSlot
    {
        internal CommonSlot3(string name, Rom appver = Rom.both) : base(name, false, 3) { }
        internal CommonSlot3(string name, string form, Rom appver = Rom.both) : base(name, form, false, 3) { }
    }
    public class CommonSlot4 : RaidBattleSlot
    {
        internal CommonSlot4(string name, Rom appver = Rom.both) : base(name, false, 4) { }
        internal CommonSlot4(string name, string form, Rom appver = Rom.both) : base(name, form, false, 4) { }
    }
    public class CommonSlot5 : RaidBattleSlot
    {
        internal CommonSlot5(string name, Rom appver = Rom.both) : base(name, true, 4) { }
        internal CommonSlot5(string name, string form, Rom appver = Rom.both) : base(name, form, true, 4) { }
    }

    public class EventSlot1 : RaidBattleSlot
    {
        internal EventSlot1(string name, Rom appver = Rom.both) : base(name, true, 1) { }
        internal EventSlot1(string name, string form, Rom appver = Rom.both) : base(name, form, true, 1) { }
    }
    public class EventSlot2 : RaidBattleSlot
    {
        internal EventSlot2(string name, Rom appver = Rom.both) : base(name, true, 2) { }
        internal EventSlot2(string name, string form, Rom appver = Rom.both) : base(name, form, true, 2) { }
    }
    public class EventSlot3 : RaidBattleSlot
    {
        internal EventSlot3(string name, Rom appver = Rom.both) : base(name, true, 3) { }
        internal EventSlot3(string name, string form, Rom appver = Rom.both) : base(name, form, true, 3) { }
    }
    public class EventSlot4 : RaidBattleSlot
    {
        internal EventSlot4(string name, Rom appver = Rom.both) : base(name, true, 4) { }
        internal EventSlot4(string name, string form, Rom appver = Rom.both) : base(name, form, true, 4) { }
    }
    public class EventSlot5 : RaidBattleSlot
    {
        internal EventSlot5(string name, Rom appver = Rom.both) : base(name, true, 5) { }
        internal EventSlot5(string name, string form, Rom appver = Rom.both) : base(name, form, true, 5) { }
    }
}
