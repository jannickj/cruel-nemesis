using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Events
{
    public class ManaCrystalSpentEvent : XmasEvent
    {
        public Player owner {get; set;}
        public Mana crystalType { get; set; }
        public ManaCrystalSpentEvent(Player p, Mana m)
        {
            owner = p;
            crystalType = m;
        }
    }
}
