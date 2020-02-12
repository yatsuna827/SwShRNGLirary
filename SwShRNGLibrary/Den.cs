using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public class Den
    {
        public readonly string Label;
        public readonly int Index;
        public readonly IReadOnlyList<RaidTable> Normal;
        public readonly IReadOnlyList<RaidTable> Rare;

        internal Den(string label, int idx, IReadOnlyList<RaidTable> normal, IReadOnlyList<RaidTable> rare)
        {
            Label = label;
            Index = idx;
            Normal = normal;
            Rare = rare;
        }
    }
}
