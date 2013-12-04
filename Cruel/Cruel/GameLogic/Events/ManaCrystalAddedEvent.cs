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
        public Player Owner {get; set;}
        public Mana CrystalType { get; set; }
        public ManaStorage Storage { get; set; }
        public ManaCrystalAddedEvent(Player p, Mana m, ManaStorage storage)
        {
            Owner = p;
            CrystalType = m;
            this.Storage = storage;
        }
    }
}
