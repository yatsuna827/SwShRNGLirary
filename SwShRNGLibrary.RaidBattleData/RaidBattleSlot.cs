using PokemonStandardLibrary.Gen8;
using PokemonStandardLibrary;
using PokemonStandardLibrary.CommonExtension;
using Newtonsoft.Json;

namespace SwShRNGLibrary.RaidBattleData
{
    public class RaidBattleSlot
    {
        public Pokemon.Species Pokemon { get; }
        public bool AllowHiddenAbility { get; }
        public int FlawlessIVs { get; }
        public bool ForceHiddenAbility { get; } // allowHiddenAbilityに依らず夢固定.
        public Gender FixedGender { get; } = Gender.Genderless;
        public Nature FixedNature { get; } = Nature.other;
        public bool ForceShiny { get; }
        public bool IsUncatchable { get; }
        public bool IsShinyLocked { get; }

        [JsonConstructor]
        internal RaidBattleSlot(string slotType, string name, 
            string fixedGender = null, 
            bool forceShiny = false, 
            bool forceHiddenAbility = false,
            bool isUncatchable = false, 
            bool isShinyLocked = false)
        {
            Pokemon = PokemonStandardLibrary.Gen8.Pokemon.GetPokemon(name);
            (FlawlessIVs, AllowHiddenAbility, ForceHiddenAbility) = GetSetting(slotType);

            ForceShiny = forceShiny;
            ForceHiddenAbility |= forceHiddenAbility;

            if (Pokemon.GenderRatio.IsFixed())
            {
                if (Pokemon.GenderRatio == GenderRatio.MaleOnly) FixedGender = Gender.Male;
                if (Pokemon.GenderRatio == GenderRatio.FemaleOnly) FixedGender = Gender.Female;
            }
            else
            {
                if (fixedGender?.ToLower() == "male" || fixedGender == "♂") FixedGender = Gender.Male;
                if (fixedGender?.ToLower() == "female" || fixedGender == "♀") FixedGender = Gender.Female;
            }

            IsUncatchable = isUncatchable;
            IsShinyLocked = isShinyLocked;
        }

        private static (int FlawlessIVs, bool AllowHiddenAbility, bool ForceHiddenAbility) GetSetting(string slotType)
        {

            switch (slotType)
            {
                default:
                case "Common1":
                    return (1, false, false);
                case "Common2":
                    return (2, false, false);
                case "Common3":
                    return (3, false, false);
                case "Common4":
                    return (4, false, false);
                case "Common5":
                    return (4, true, false);

                case "Rare1":
                    return (1, true, true);
                case "Rare2":
                    return (2, true, true);
                case "Rare3":
                    return (3, true, true);
                case "Rare4":
                    return (4, true, true);
                case "Rare5":
                    return (5, true, true);

                case "Event1":
                    return (1, true, false);
                case "Event2":
                    return (2, true, false);
                case "Event3":
                    return (3, true, false);
                case "Event4":
                    return (4, true, false);
                case "Event5":
                    return (5, true, false);
            }
        }
    }
}
