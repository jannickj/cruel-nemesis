using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events.UnitEvents
{
    public class UnitRevivedEvent : XmasEvent
    {
        private Unit.UnitEntity unitEntity;

        public Unit.UnitEntity UnitEntity
        {
            get { return unitEntity; }
        }

        public UnitRevivedEvent(Unit.UnitEntity unitEntity)
        {
            // TODO: Complete member initialization
            this.unitEntity = unitEntity;
        }
    }
}
