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

        public static IReadOnlyList<WildAreaMap> GetWildAreaMapList(Rom version, Region region = Region.Mainland)
        {
            return version == Rom.Sword ? SwordMapList[(int)region] : ShieldMapList[(int)region];
        }
        private static readonly IReadOnlyList<WildAreaMap>[] SwordMapList;
        private static readonly IReadOnlyList<WildAreaMap>[] ShieldMapList;


        static WildAreaMap()
        {
            SwordMapList = new IReadOnlyList<WildAreaMap>[3];
            ShieldMapList = new IReadOnlyList<WildAreaMap>[3];
            SwordMapList[0] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.MainlandAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            SwordMapList[1] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.IsleOfArmorAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            SwordMapList[2] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.CrownTundraAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            ShieldMapList[0] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.MainlandAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
            ShieldMapList[1] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.IsleOfArmorAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
            ShieldMapList[2] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.CrownTundraAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
        }
    }
}