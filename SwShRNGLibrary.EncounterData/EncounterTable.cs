using PokemonStandardLibrary.Gen8;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwShRNGLibrary.EncounterData
{
    // ひとつのテーブルを表現するクラス。
    public class RandomEncounterTable
    {
        public uint BasicLv { get; }
        public uint VariableLv { get; }
        public IReadOnlyList<(uint Rate, Pokemon.Species Pokemon)> Table { get; }
        public bool ToBeStrengthenedAfterClearing { get; }

        internal RandomEncounterTable(RandomEncounterRawData raw, bool strengthen)
        {
            BasicLv = raw.BasicLv;
            VariableLv = raw.VariableLv;
            Table = raw.Table;
            ToBeStrengthenedAfterClearing = strengthen;
        }
    }
    public class StaticEncounterData
    {
        public Pokemon.Species Pokemon { get; }
        public uint Lv { get; }
        public bool ToBeStrengthenedAfterClearing { get; }
        public bool ShinyLocked { get; }
        public uint? FixedAbility { get; }
        public uint FlawlessIVs { get; }

        internal StaticEncounterData(StaticEncounterRawData raw, bool strengthen)
        {
            Pokemon = raw.Pokemon;
            Lv = raw.Lv;
            ShinyLocked = raw.ShinyLocked;
            FixedAbility = raw.FixedAbility;
            FlawlessIVs = raw.FlawlessIVs;
            ToBeStrengthenedAfterClearing = strengthen;
        }
    }

}
