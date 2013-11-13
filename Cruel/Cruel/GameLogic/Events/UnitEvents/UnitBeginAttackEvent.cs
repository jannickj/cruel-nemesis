using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events.UnitEvents
{
    public class UnitBeginAttackEvent : XmasEvent
	{
        private XmasEngineModel.EntityLib.XmasEntity Source;
        private Unit.UnitEntity Target;
        private int dmg;
        private int attackTime;

        public UnitBeginAttackEvent(XmasEngineModel.EntityLib.XmasEntity Source, Unit.UnitEntity Target, int dmg, int attackTime)
        {
            // TODO: Complete member initialization
            this.Source = Source;
            this.Target = Target;
            this.dmg = dmg;
            this.attackTime = attackTime;
        }
	}
}
