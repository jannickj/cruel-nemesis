using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using Assets.GameLogic.Modules;

namespace Assets.GameLogic.Unit
{
	public abstract class UnitEntity : XmasEntity 
	{

        public UnitEntity(Player owner)
        {
            this.RegisterModule(new UnitInfoModule(owner));
            this.RegisterModule(new MoveModule(5));
            this.RegisterModule(new HealthModule(10));
            this.RegisterModule(new AttackModule());
            this.Module<AttackModule>().AttackRange = 1;
        }

        public virtual Type getUnitType()
        {
            return this.GetType();
        }
	}
}
