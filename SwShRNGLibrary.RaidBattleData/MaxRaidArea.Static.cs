using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SwShRNGLibrary.RaidBattleData.Properties;

namespace SwShRNGLibrary.RaidBattleData
{
    public partial class MaxRaidArea
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IReadOnlyList<MaxRaidArea> GetAreaList(Rom version, Region region = Region.WildArea)
        {
            return version == Rom.Sword ? swordMapList[(int)region] : shieldMapList[(int)region];
        }


        public static MaxRaidArea GetMap(Rom version, Region region, int index)
        {
            if (swordMapList[(int)region].Count <= index) throw new IndexOutOfRangeException($"index = {index}は範囲外です.");
            return version == Rom.Sword ? swordMapList[(int)region][index] : shieldMapList[(int)region][index];
        }

        /// <summary>
        /// エリア名からマップ情報を取得します.
        /// エリア名はUniqueなのでリージョン指定は不要です.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="mapName"></param>
        /// <returns></returns>
        public static MaxRaidArea GetMap(Rom version, string mapName)
        {
            if (!swordMapDicrionary.ContainsKey(mapName)) throw new KeyNotFoundException($"マップ名<{mapName}>が登録されていません. typoや漢字変換ミスを確認してください.");
            return version == Rom.Sword ? swordMapDicrionary[mapName] : shieldMapDicrionary[mapName];
        }

        /// <summary>
        /// そのリージョンのマップ名をすべて取得します.
        /// ただし巣穴が無いマップは省略されているので注意してください. 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IReadOnlyList<string> GetMapNameList(Region region) => mapNameList[(int)region];

        private static readonly IReadOnlyList<MaxRaidArea>[] swordMapList;
        private static readonly IReadOnlyList<MaxRaidArea>[] shieldMapList;
        private static readonly IReadOnlyList<string>[] mapNameList;
        private static readonly Dictionary<string, MaxRaidArea> swordMapDicrionary;
        private static readonly Dictionary<string, MaxRaidArea> shieldMapDicrionary;

        static MaxRaidArea()
        {
            swordMapList = new IReadOnlyList<MaxRaidArea>[3];
            shieldMapList = new IReadOnlyList<MaxRaidArea>[3];
            swordMapList[0] = JsonConvert.DeserializeObject<AreaPasser[]>(Resources.WildAreaAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            swordMapList[1] = JsonConvert.DeserializeObject<AreaPasser[]>(Resources.IsleOfArmorAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            swordMapList[2] = JsonConvert.DeserializeObject<AreaPasser[]>(Resources.CrownTundraAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            shieldMapList[0] = JsonConvert.DeserializeObject<AreaPasser[]>(Resources.WildAreaAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
            shieldMapList[1] = JsonConvert.DeserializeObject<AreaPasser[]>(Resources.IsleOfArmorAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
            shieldMapList[2] = JsonConvert.DeserializeObject<AreaPasser[]>(Resources.CrownTundraAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();

            mapNameList = swordMapList.Select(_ => _.Select(__ => __.MapName).ToArray()).ToArray();

            var dic_sw = new Dictionary<string, MaxRaidArea>();
            foreach (var region in swordMapList)
                foreach (var map in region)
                    dic_sw.Add(map.MapName, map);

            var dic_sh = new Dictionary<string, MaxRaidArea>();
            foreach (var region in shieldMapList)
                foreach (var map in region)
                    dic_sh.Add(map.MapName, map);

            swordMapDicrionary = dic_sw;
            shieldMapDicrionary = dic_sh;
        }
    }
}
