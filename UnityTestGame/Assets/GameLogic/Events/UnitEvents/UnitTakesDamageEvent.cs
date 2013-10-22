using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events.UnitEvents
{
    public class UnitTakesDamageEvent : XmasEvent
	{
        private Unit.UnitEntity dmgtaker;
        private XmasEngineModel.EntityLib.XmasEntity dmgdealer;
        private int Damage;

        public UnitTakesDamageEvent(Unit.UnitEntity dmgtaker, XmasEngineModel.EntityLib.XmasEntity dmgdealer, int Damage)
        {
            // TODO: Complete member initialization
            this.dmgtaker = dmgtaker;
            this.dmgdealer = dmgdealer;
            this.Damage = Damage;
        }
	}
}
