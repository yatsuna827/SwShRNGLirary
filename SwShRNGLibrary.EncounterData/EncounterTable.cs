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

        internal RandomEncounterTable(RandomEncounterRawData raw)
        {
            BasicLv = raw.BasicLv;
            VariableLv = raw.VariableLv;
            Table = raw.Table;
        }
    }
    public class StaticEncounterData
    {
        public Pokemon.Species Pokemon { get; }
        public uint Lv { get; }
        public bool ShinyLocked { get; }
        public uint? FixedAbility { get; }
        public uint FlawlessIVs { get; }

        internal StaticEncounterData(StaticEncounterRawData raw)
        {
            Pokemon = raw.Pokemon;
            Lv = raw.Lv;
            ShinyLocked = raw.ShinyLocked;
            FixedAbility = raw.FixedAbility;
            FlawlessIVs = raw.FlawlessIVs;
        }
    }

}
