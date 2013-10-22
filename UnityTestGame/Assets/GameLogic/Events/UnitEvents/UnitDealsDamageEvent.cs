using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events.UnitEvents
{
    public class UnitDealsDamageEvent : XmasEvent
	{
        private XmasEngineModel.EntityLib.XmasEntity Source;
        private Unit.UnitEntity Target;
        private int Damage;

        public UnitDealsDamageEvent(XmasEngineModel.EntityLib.XmasEntity Source, Unit.UnitEntity Target, int Damage)
        {
            // TODO: Complete member initialization
            this.Source = Source;
            this.Target = Target;
            this.Damage = Damage;
        }
	}
}
