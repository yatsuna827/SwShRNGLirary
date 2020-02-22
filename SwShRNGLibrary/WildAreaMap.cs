using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using SwShRNGLibrary.Properties;

namespace SwShRNGLibrary
{
    public class WildAreaMap
    {
        public readonly string MapName;
        public readonly IReadOnlyList<Den> DenList;

        internal WildAreaMap(string name, Den[] denList)
        {
            MapName = name;
            DenList = denList;
        }

        public static IReadOnlyList<WildAreaMap> GetWildAreaMapList(int version)
        {
            return version % 2 == 0 ? SwordMapList : ShieldMapList;
        }
        private static readonly IReadOnlyList<WildAreaMap> SwordMapList;
        private static readonly IReadOnlyList<WildAreaMap> ShieldMapList;
        static WildAreaMap()
        {
            SwordMapList = JsonSerializer.Deserialize<AreaPasser[]>(Resources.AreaList_sw).Select(_ => _.createArea(RaidTable.SwordTables)).ToArray();
            ShieldMapList = JsonSerializer.Deserialize<AreaPasser[]>(Resources.AreaList_sh).Select(_ => _.createArea(RaidTable.ShieldTables)).ToArray();
        }
    }
}