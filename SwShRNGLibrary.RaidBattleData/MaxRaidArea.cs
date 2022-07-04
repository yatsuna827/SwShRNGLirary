using System.Collections.Generic;
using System.Linq;

namespace SwShRNGLibrary.RaidBattleData
{
    public partial class MaxRaidArea
    {
        public string MapName { get; }
        public IReadOnlyList<Den> DenList { get; }

        private readonly string[] _denLabels;
        private readonly Dictionary<string, Den> _denDict;
        public IReadOnlyList<string> GetDenLabels() => _denLabels;
        public Den this[string label] { get => _denDict.ContainsKey(label) ? _denDict[label] : throw new KeyNotFoundException($"巣穴{label}は存在しません."); }

        internal MaxRaidArea(string name, Den[] denList)
        {
            MapName = name;
            DenList = denList;
            _denLabels = denList.Select(_ => _.Label).ToArray();
            _denDict = denList.ToDictionary(_ => _.Label, _ => _);
        }
    }

}