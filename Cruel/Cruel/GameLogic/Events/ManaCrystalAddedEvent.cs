using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;
using CruelTest.SpellSystem;

namespace Cruel.GameLogic.Events
{
    public class ManaCrystalAddedEvent : XmasEvent
    {
        public Player owner {get; set;}
        public Mana crystalType { get; set; }
        public ManaStorage storage { get; set; }
        public ManaCrystalAddedEvent(Player p, Mana m, ManaStorage storage)
        {
            owner = p;
            crystalType = m;
            this.storage = storage;
        }
    }
}
