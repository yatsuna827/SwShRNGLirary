using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PokemonStandardLibrary.Gen8;

namespace SwShRNGLibrary.EncounterData
{
    // 生のJSONを受けるためのクラス。Tableの構造やWeatherの変換なども受け持つ。
    class RandomEncounterRawData
    {
        public string MapName { get; }
        public Weather Weather { get; }

        public byte BasicLv { get; }
        public byte VariableLv { get; }
        public (uint Rate, Pokemon.Species Pokemon)[] Table { get; }

        [JsonConstructor]
        internal RandomEncounterRawData(string mapName, byte basicLv, byte variableLv, RawTable table, int weather = 0)
        {
            MapName = mapName;
            Weather = (Weather)weather;
            BasicLv = basicLv;
            VariableLv = variableLv;
            Table = table.Content.Zip(table.Rate, (c, r) => (r, Pokemon.GetPokemon(c))).ToArray();
        }
        internal class RawTable
        {
            public string[] Content { get; set; }
            public uint[] Rate { get; set; }
        }
    }
    class StaticEncounterRawData
    {
        public string MapName { get; }
        public Weather Weather { get; }

        public Pokemon.Species Pokemon { get; }
        public uint Lv { get; }
        public bool ShinyLocked { get; }
        public uint? FixedAbility { get; }
        public uint FlawlessIVs { get; }

        [JsonConstructor]
        internal StaticEncounterRawData(string mapName, int weather, string name, uint lv,
            bool shinyLocked = false, uint? fixedAbility = null, uint flawlessIVs = 0)
        {
            MapName = mapName;
            Weather = (Weather)weather;
            Pokemon = PokemonStandardLibrary.Gen8.Pokemon.GetPokemon(name);
            Lv = lv;
            ShinyLocked = shinyLocked;
            FixedAbility = fixedAbility;
            FlawlessIVs = flawlessIVs;
        }
    }

}
