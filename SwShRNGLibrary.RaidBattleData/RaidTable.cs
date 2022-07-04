using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SwShRNGLibrary.RaidBattleData
{
    // バージョンごと・タイプ別のレイドテーブルをまとめたクラス.
    public partial class RaidTable : IReadOnlyList<RaidBattleSlot>
    {
        private readonly RaidBattleSlot[] table;
        public RaidBattleSlot this[int i] { get => table[i]; }
        public int Count { get => table.Length; }

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
    }

}
