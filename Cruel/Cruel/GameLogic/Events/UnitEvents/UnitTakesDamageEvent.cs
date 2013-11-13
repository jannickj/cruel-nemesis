using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic.Unit;

namespace Cruel.GameLogic.Events.UnitEvents
{
    public class UnitTakesDamageEvent : XmasEvent
	{
        private UnitEntity dmgtaker;
        private UnitEntity dmgdealer;
        private int Damage;

        public UnitTakesDamageEvent(UnitEntity dmgtaker, UnitEntity dmgdealer, int Damage)
        {
            // TODO: Complete member initialization
            this.dmgtaker = dmgtaker;
            this.dmgdealer = dmgdealer;
            this.Damage = Damage;
        }
	}
}
