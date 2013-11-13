using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events.UnitEvents
{
	public class UnitDeclaredAsAttacked : XmasEvent
	{
        private Unit.UnitEntity attacked;
        private Unit.UnitEntity attackee;
        private bool p;

        public UnitDeclaredAsAttacked(Unit.UnitEntity attacked, Unit.UnitEntity attackee, bool p)
        {
            // TODO: Complete member initialization
            this.attacked = attacked;
            this.attackee = attackee;
            this.p = p;
        }
	}
}
