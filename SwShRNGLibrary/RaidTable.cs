using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using SwShRNGLibrary.Properties;

namespace SwShRNGLibrary
{
    // バージョンごと・タイプ別のレイドテーブルをまとめたクラス.
    public class RaidTable : IReadOnlyList<RaidBattleSlot>
    {
        private readonly RaidBattleSlot[] table;
        public RaidBattleSlot this[int i] { get { return table[i]; } }
        public int Count { get { return table.Length; } }

        public IEnumerator<RaidBattleSlot> GetEnumerator()
        {
            return table.AsEnumerable().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return table.AsEnumerable().GetEnumerator();
        }

        internal RaidTable(params RaidBattleSlot[] table)
        {
            this.table = table;
        }

        internal static Dictionary<string, IReadOnlyList<RaidTable>> SwordTables;
        internal static Dictionary<string, IReadOnlyList<RaidTable>> ShieldTables;

        public static readonly IReadOnlyList<RaidTable> DefaultTable = new RaidTable[] {
            new RaidTable(new EventSlot1("ヌオー")),
            new RaidTable(new EventSlot2("ヌオー")),
            new RaidTable(new EventSlot3("ヌオー")),
            new RaidTable(new EventSlot4("ヌオー")),
            new RaidTable(new EventSlot5("ヌオー")),
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
                var d = JsonSerializer.Deserialize<TableConverter[]>(data);
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
                var d = JsonSerializer.Deserialize<TableConverter[]>(data);
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
