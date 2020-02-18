﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;

namespace SwShRNGLibrary
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
            var t = JsonSerializer.Deserialize<EventTableConverter>(json);
            eventTableList[0].Add(t.GetTable(0));
            eventTableList[1].Add(t.GetTable(1));
            eventLabelList.Add(t.Label);
        }
        public IReadOnlyList<IReadOnlyList<RaidTable>> GetEventList(int version)
        {
            return eventTableList[version];
        }
        public IReadOnlyList<string> GetEventLabelList()
        {
            return eventLabelList;
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
    class EventTableConverter
    {
        public string Label { get; set; }
        public SlotPasser[][] SwTable { get; set; }
        public SlotPasser[][] ShTable { get; set; }
        public IReadOnlyList<RaidTable> GetTable(int version)
        {
            return (version == 0 ? SwTable.Select(_ => new RaidTable(_.Select(p => p.createSlot()).ToArray())) : ShTable.Select(_ => new RaidTable(_.Select(p => p.createSlot()).ToArray()))).ToArray();
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

                case "Event1": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 1) : new RaidBattleSlot(Name, Form, true, 1); break;
                case "Event2": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 2) : new RaidBattleSlot(Name, Form, true, 2); break;
                case "Event3": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 3) : new RaidBattleSlot(Name, Form, true, 3); break;
                case "Event4": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 4) : new RaidBattleSlot(Name, Form, true, 4); break;
                case "Event5": 
                    slot = Form == "" ? new RaidBattleSlot(Name, true, 5) : new RaidBattleSlot(Name, Form, true, 5); break;
            }
            if (ForceShiny) slot = slot.BeForceShiny();
            if (ForceHiddenAbility) slot = slot.BeForceHiddenAbility();

            return slot;
        }
    }
}