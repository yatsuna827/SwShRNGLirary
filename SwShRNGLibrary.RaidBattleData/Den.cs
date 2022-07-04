using System.Collections.Generic;

namespace SwShRNGLibrary.RaidBattleData
{
    public class Den
    {
        public string Label { get; }
        public int Index { get; }
        public IReadOnlyList<RaidTable> Normal { get; }
        public IReadOnlyList<RaidTable> Rare { get; }

        internal Den(string label, int idx, IReadOnlyList<RaidTable> normal, IReadOnlyList<RaidTable> rare)
        {
            Label = label;
            Index = idx;
            Normal = normal;
            Rare = rare;
        }
    }
}
