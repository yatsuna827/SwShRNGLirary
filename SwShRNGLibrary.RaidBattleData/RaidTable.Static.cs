using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SwShRNGLibrary.RaidBattleData.Properties;

namespace SwShRNGLibrary.RaidBattleData
{
    public partial class RaidTable
    {
        internal static Dictionary<string, IReadOnlyList<RaidTable>> SwordTables { get; set; }
        internal static Dictionary<string, IReadOnlyList<RaidTable>> ShieldTables { get; set; }

        public static IReadOnlyList<RaidTable> GetTable(Rom version, string key)
        {
            var t = version == Rom.Sword ? SwordTables : ShieldTables;
            if (!t.ContainsKey(key)) throw new KeyNotFoundException($"{key}は存在しません");

            return t[key];
        }
        public static IEnumerable<string> EnumerateKeys(Rom version, string key)
        {
            var t = version == Rom.Sword ? SwordTables : ShieldTables;
            return t.Select(_ => _.Key).Where(__ => __.Contains(key));
        }

        public static readonly IReadOnlyList<RaidTable> DefaultTable = new RaidTable[] {
            new RaidTable(new RaidBattleSlot("Event1", "ヌオー")),
            new RaidTable(new RaidBattleSlot("Event2", "ヌオー")),
            new RaidTable(new RaidBattleSlot("Event3", "ヌオー")),
            new RaidTable(new RaidBattleSlot("Event4", "ヌオー")),
            new RaidTable(new RaidBattleSlot("Event5", "ヌオー")),
        };

        static RaidTable()
        {
            var swdata = new string[]
            {
                Resources.Bug_sw,
                Resources.Dark_sw,
                Resources.Dragon_sw,
                Resources.Electric_sw,
                Resources.Fairy_sw,
                Resources.Fighting_sw,
                Resources.Fire_sw,
                Resources.Flying_sw,
                Resources.Ghost_sw,
                Resources.Grass_sw,
                Resources.Ground_sw,
                Resources.Ice_sw,
                Resources.Normal_sw,
                Resources.Poison_sw,
                Resources.Psychic_sw,
                Resources.Rock_sw,
                Resources.Steel_sw,
                Resources.Water_sw,
                Resources.Concept_sw
            };
            var swTable = new Dictionary<string, IReadOnlyList<RaidTable>>();
            foreach (var data in swdata)
            {
                var d = JsonConvert.DeserializeObject<TableConverter[]>(data);
                foreach (var t in d)
                {
                    var table = t.GetTable();
                    swTable.Add(t.Label, table);
                }
            }
            SwordTables = swTable;

            var shdata = new string[]
            {
                Resources.Bug_sh,
                Resources.Dark_sh,
                Resources.Dragon_sh,
                Resources.Electric_sh,
                Resources.Fairy_sh,
                Resources.Fighting_sh,
                Resources.Fire_sh,
                Resources.Flying_sh,
                Resources.Ghost_sh,
                Resources.Grass_sh,
                Resources.Ground_sh,
                Resources.Ice_sh,
                Resources.Normal_sh,
                Resources.Poison_sh,
                Resources.Psychic_sh,
                Resources.Rock_sh,
                Resources.Steel_sh,
                Resources.Water_sh,
                Resources.Concept_sh
            };
            var shTable = new Dictionary<string, IReadOnlyList<RaidTable>>();
            foreach (var data in shdata)
            {
                var d = JsonConvert.DeserializeObject<TableConverter[]>(data);
                foreach (var t in d)
                {
                    var table = t.GetTable();
                    shTable.Add(t.Label, table);
                }
            }
            ShieldTables = shTable;
        }
    }
}
