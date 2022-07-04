using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SwShRNGLibrary.RaidBattleData
{
    public class EventRaid
    {
        private readonly List<string> eventLabelList;
        private readonly List<IReadOnlyList<RaidTable>>[] eventTableList;
        public EventRaid()
        {
            eventLabelList = new List<string>();
            eventTableList = new List<IReadOnlyList<RaidTable>>[]
            {
                new List<IReadOnlyList<RaidTable>>(),
                new List<IReadOnlyList<RaidTable>>()
            };
        }
        public void Import(string json)
        {
            var t = JsonConvert.DeserializeObject<EventTableConverter>(json);
            eventTableList[0].Add(t.GetTable(Rom.Sword));
            eventTableList[1].Add(t.GetTable(Rom.Shield));
            eventLabelList.Add(t.Label);
        }
        public IReadOnlyList<IReadOnlyList<RaidTable>> GetEventList(Rom version)
        {
            return eventTableList[(int)version];
        }

        // 高々数十個しかデータ登録されないはずなので線形探索で事足りるという判断をしました.
        public IReadOnlyList<RaidTable> GetEventRaidTable(Rom version, string label)
        {
            var idx = eventLabelList.FindIndex(_ => _ == label);
            if (idx < 0) throw new KeyNotFoundException($"イベントデータ【{label}】が見つかりませんでした");
            return eventTableList[(int)version][idx];
        }
        public IReadOnlyList<string> GetEventLabelList()
        {
            return eventLabelList;
        }
    }

    // 絶対こいつら全部消せるんだけど、壊れそうなので保留。
    class AreaPasser
    {
        public string AreaName { get; set; }
        public DenPasser[] DenList { get; set; }
        internal MaxRaidArea CreateArea(Dictionary<string, IReadOnlyList<RaidTable>> table)
        {
            return new MaxRaidArea(AreaName, DenList.Select(_ => new Den(_.Label, _.Index, table[_.Normal], table[_.Rare])).ToArray());
        }
    }
    class DenPasser
    {
        public string Label { get; set; }
        public int Index { get; set; }
        public string Normal { get; set; }
        public string Rare { get; set; }
    }
    class TableConverter
    {
        public string Label { get; set; }
        public RaidBattleSlot[][] Table { get; set; }
        public IReadOnlyList<RaidTable> GetTable()
        {
            return Table.Select(_ => new RaidTable(_)).ToArray();
        }
    }
    class EventTableConverter
    {
        public string Label { get; set; }
        public RaidBattleSlot[][] SwTable { get; set; }
        public RaidBattleSlot[][] ShTable { get; set; }
        public IReadOnlyList<RaidTable> GetTable(Rom version)
        {
            return (version == Rom.Sword ? SwTable.Select(_ => new RaidTable(_)) : ShTable.Select(_ => new RaidTable(_))).ToArray();
        }
    }
}