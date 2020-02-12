using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;

namespace SwShRNGLibrary
{
    public class EventRaid
    {
        private List<(string Label, IReadOnlyList<RaidTable> Table)> eventTableList;
        public EventRaid()
        {
            eventTableList = new List<(string Label, IReadOnlyList<RaidTable> Table)>();
        }
        public void Import(string json)
        {
            var t = JsonSerializer.Deserialize<TableConverter>(json);
            eventTableList.Add((t.Label, t.GetTable()));
        }
        public IReadOnlyList<(string Label, IReadOnlyList<RaidTable> Table)> GetEventList()
        {
            return eventTableList;
        }
    }
    class AreaPasser
    {
        public string AreaName { get; set; }
        public DenPasser[] DenList { get; set; }
        internal WildAreaMap createArea(Dictionary<string, IReadOnlyList<RaidTable>> table)
        {
            return new WildAreaMap(AreaName, DenList.Select(_ => new Den(_.Label, _.Index, table[_.Normal], table[_.Rare])).ToArray());
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
        public SlotPasser[][] Table { get; set; }
        public IReadOnlyList<RaidTable> GetTable()
        {
            return Table.Select(_ => new RaidTable(_.Select(p => p.createSlot()).ToArray())).ToArray();
        }
    }
    class SlotPasser
    {
        public string SlotType { get; set; }
        public string Name { get; set; }
        public string Form { get; set; } = "";
        public bool ForceShiny { get; set; }
        public bool ForceHiddenAbility { get; set; }
        internal RaidBattleSlot createSlot()
        {
            RaidBattleSlot slot;
            switch (SlotType)
            {
                default:
                case "Common1": 
                    slot = Form == "" ? new RaidBattleSlot(Name, false, 1) : new RaidBattleSlot(Name, Form, false, 1); break;
                case "Common2": 
                    slot = Form == "" ? new RaidBattleSlot(Name, false, 2) : new RaidBattleSlot(Name, Form, false, 2); break;
                case "Common3":
                    slot = Form == "" ? new RaidBattleSlot(Name, false, 3) : new RaidBattleSlot(Name, Form, false, 3); break;
                case "Common4": 
                    slot = Form == "" ? new RaidBattleSlot(Name, false, 4) : new RaidBattleSlot(Name, Form, false, 4); break;
                case "Common5": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 4) : new RaidBattleSlot(Name, Form, true, 4); break;

                case "Enevt1": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 1) : new RaidBattleSlot(Name, Form, true, 1); break;
                case "Enevt2": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 2) : new RaidBattleSlot(Name, Form, true, 2); break;
                case "Enevt3": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 3) : new RaidBattleSlot(Name, Form, true, 3); break;
                case "Enevt4": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 4) : new RaidBattleSlot(Name, Form, true, 4); break;
                case "Enevt5": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 5) : new RaidBattleSlot(Name, Form, true, 5); break;
            }
            if (ForceShiny) slot = slot.BeForceShiny();
            if (ForceHiddenAbility) slot = slot.BeForceHiddenAbility();

            return slot;
        }
    }
}