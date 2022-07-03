using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PokemonStandardLibrary.Gen8;
using SwShRNGLibrary;
using static SwShRNGLibrary.EncounterData.Properties.Resources;

namespace SwShRNGLibrary.EncounterData
{
    public static class EncounterData
    {
        private static RandomEncounter[] ConvertRE(string src)
            => JsonConvert.DeserializeObject<RandomEncounterRawData[]>(src).GroupBy(_ => _.MapName).Select(_ => new RandomEncounter(_.Key, _.ToArray())).ToArray();
        private static FishingEncounter[] ConvertFE(string src)
            => JsonConvert.DeserializeObject<RandomEncounterRawData[]>(src).Select(_ => new FishingEncounter(_)).ToArray();
        private static StaticEncounter[] ConvertSE(string src)
            => JsonConvert.DeserializeObject<StaticEncounterRawData[]>(src).GroupBy(_ => _.MapName).Select(_ => new StaticEncounter(_.Key, _.ToArray())).ToArray();
        private static LimitedEncounter[] ConvertLE(string src)
            => JsonConvert.DeserializeObject<StaticEncounterRawData[]>(src).GroupBy(_ => _.MapName).Select(_ => new LimitedEncounter(_.Key, _.ToArray())).ToArray();

        public static readonly EncounterData<RandomEncounter> Symbol = new EncounterData<RandomEncounter>(ConvertRE(Symbol_sw), ConvertRE(Symbol_sh));
        public static readonly EncounterData<RandomEncounter> Hidden = new EncounterData<RandomEncounter>(ConvertRE(Hidden_sw), ConvertRE(Hidden_sh));
        public static readonly EncounterData<FishingEncounter> Fishing = new EncounterData<FishingEncounter>(ConvertFE(Fishing_sw), ConvertFE(Fishing_sh));
        public static readonly EncounterData<StaticEncounter> Static = new EncounterData<StaticEncounter>(ConvertSE(Static_sw), ConvertSE(Static_sh));
        public static readonly EncounterData<LimitedEncounter> Limited = new EncounterData<LimitedEncounter>(ConvertLE(Limited_sw), ConvertLE(Limited_sh));
    }
    public sealed class EncounterData<T>
        where T : IEncounterData
    {
        public T[] All(Rom rom)
        {
            if (rom == Rom.Sword) return _sw;
            if (rom == Rom.Shield) return _sh;

            throw new ArgumentOutOfRangeException();
        }
        public IEnumerable<string> GetMapNames() => _sw.Select(_ => _.MapName);
        public T GetMap(Rom rom, string mapName)
        {
            var dict = rom == Rom.Sword ? _swDict : 
                       rom == Rom.Shield ? _shDict : 
                       throw new ArgumentException();

            if (!dict.ContainsKey(mapName)) return default;
            return dict[mapName];
        }

        private readonly T[] _sw, _sh;
        private readonly Dictionary<string, T> _swDict, _shDict;

        internal EncounterData(T[] sw, T[] sh)
        {
            _sw = sw;
            _sh = sh;

            _swDict = _sw.ToDictionary(_ => _.MapName, _ => _);
            _shDict = _sh.ToDictionary(_ => _.MapName, _ => _);
        }
    }
}
