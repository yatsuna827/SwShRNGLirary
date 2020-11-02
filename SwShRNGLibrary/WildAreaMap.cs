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
        private readonly IReadOnlyList<string> denLabelList;
        private readonly Dictionary<string, Den> denDictionary;
        public IReadOnlyList<string> GetDenLabels() => denLabelList;
        public Den this[string label] { get { if (!denDictionary.ContainsKey(label)) throw new KeyNotFoundException($"巣穴{label}は存在しません."); return denDictionary[label]; } }

        internal WildAreaMap(string name, Den[] denList)
        {
            MapName = name;
            DenList = denList;
            denLabelList = denList.Select(_ => _.Label).ToArray();
            denDictionary = denList.ToDictionary(_ => _.Label, _ => _);
        }







        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IReadOnlyList<WildAreaMap> GetWildAreaMapList(Rom version, Region region = Region.Mainland)
        {
            return version == Rom.Sword ? swordMapList[(int)region] : shieldMapList[(int)region];
        }
        

        public static WildAreaMap GetMap(Rom version, Region region, int index)
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
        public static WildAreaMap GetMap(Rom version, string mapName)
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

        private static readonly IReadOnlyList<WildAreaMap>[] swordMapList;
        private static readonly IReadOnlyList<WildAreaMap>[] shieldMapList;
        private static readonly IReadOnlyList<string>[] mapNameList;
        private static readonly Dictionary<string, WildAreaMap> swordMapDicrionary;
        private static readonly Dictionary<string, WildAreaMap> shieldMapDicrionary;

        static WildAreaMap()
        {
            swordMapList = new IReadOnlyList<WildAreaMap>[3];
            shieldMapList = new IReadOnlyList<WildAreaMap>[3];
            swordMapList[0] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.MainlandAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            swordMapList[1] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.IsleOfArmorAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            swordMapList[2] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.CrownTundraAreaList_sw).Select(_ => _.CreateArea(RaidTable.SwordTables)).ToArray();
            shieldMapList[0] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.MainlandAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
            shieldMapList[1] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.IsleOfArmorAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();
            shieldMapList[2] = JsonSerializer.Deserialize<AreaPasser[]>(Resources.CrownTundraAreaList_sh).Select(_ => _.CreateArea(RaidTable.ShieldTables)).ToArray();

            mapNameList = swordMapList.Select(_ => _.Select(__ => __.MapName).ToArray()).ToArray();

            var dic_sw = new Dictionary<string, WildAreaMap>();
            foreach (var region in swordMapList)
                foreach (var map in region)
                    dic_sw.Add(map.MapName, map);

            var dic_sh = new Dictionary<string, WildAreaMap>();
            foreach (var region in shieldMapList)
                foreach (var map in region)
                    dic_sh.Add(map.MapName, map);

            swordMapDicrionary = dic_sw;
            shieldMapDicrionary = dic_sh;
        }
    }
}