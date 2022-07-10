﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwShRNGLibrary.EncounterData
{
    // マップごとのデータをまとめるクラス。
    public interface IEncounterData
    {
        string MapName { get; }
    }

    public sealed class RandomEncounter : IEncounterData
    {
        public string MapName { get; }

        private readonly (Weather Weather, RandomEncounterTable Data)[] _data;
        public RandomEncounterTable this[Weather w] { get => _data[(int)w].Data; }
        public IEnumerable<(Weather Weather, RandomEncounterTable Data)> All() => _data;

        internal RandomEncounter(string mapName, RandomEncounterRawData[] datas)
        {
            MapName = mapName;
            _data = datas.Select((_, i) => ((Weather)i, new RandomEncounterTable(_, _.Location == Location.WildArea || _.Location == Location.IsleOfArmor))).ToArray();
        }
    }
    public sealed class FishingEncounter : IEncounterData
    {
        public string MapName { get; }
        public RandomEncounterTable Data { get; }

        internal FishingEncounter(RandomEncounterRawData data)
        {
            MapName = data.MapName;
            Data = new RandomEncounterTable(data, data.Location == Location.WildArea || data.Location == Location.IsleOfArmor);
        }
    }
    public sealed class StaticEncounter : IEncounterData
    {
        public string MapName { get; }

        private readonly (Weather Weather, StaticEncounterData[] Data)[] _data;
        public IReadOnlyList<StaticEncounterData> this[Weather w] { get => _data[(int)w].Data; }
        public IEnumerable<(Weather Weather, StaticEncounterData[] Data)> All() => _data.Where(_ => _.Data.Length > 0);

        internal StaticEncounter(string mapName, StaticEncounterRawData[] datas)
        {
            MapName = mapName;
            var tmp = datas.GroupBy(_ => _.Weather);
            _data = Enumerable.Range(0, 9).Select(_ => ((Weather)_, new StaticEncounterData[0])).ToArray();
            foreach (var row in tmp)
            {
                // ガラル本土とカンムリ雪原はクリア後のレベル変動なし
                // ワイルドエリアとヨロイ島は一部を除いてレベルが60に強化される
                _data[(int)row.Key] = (row.Key, row.Select(_ => new StaticEncounterData(_,
                    (_.Location == Location.WildArea || _.Location == Location.IsleOfArmor) && _.ToBeStrengthen)).ToArray());
            }
        }
    }
    public sealed class LimitedEncounter : IEncounterData
    {
        public string MapName { get; }
        public IReadOnlyList<StaticEncounterData> Data { get; }

        internal LimitedEncounter(string mapName, StaticEncounterRawData[] raw)
        {
            MapName = mapName;
            Data = raw.Select(_ => new StaticEncounterData(_, false)).ToArray();
        }
    }

}
