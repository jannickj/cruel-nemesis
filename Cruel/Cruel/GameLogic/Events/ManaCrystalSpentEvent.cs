using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;
using CruelTest.SpellSystem;

namespace Cruel.GameLogic.Events
{
    public class ManaCrystalSpentEvent : XmasEvent
    {
        public Player Owner {get; set;}
        public Mana CrystalType { get; set; }
        public ManaStorage Storage { get; set; }
        public ManaCrystalSpentEvent(Player p, Mana m, ManaStorage storage)
        {
            Owner = p;
            CrystalType = m;
            Storage = storage;
        }
    }
}
