using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Unit;

namespace Assets.GameLogic.Actions
{
    public class DealDamageToUnitAction : EntityXmasAction
	{
        public UnitEntity target { get; private set; }
        public int dmg { get; private set; }

        public DealDamageToUnitAction(UnitEntity target, int dmg)
        {
            this.target = target;
            this.dmg = dmg;
        }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
