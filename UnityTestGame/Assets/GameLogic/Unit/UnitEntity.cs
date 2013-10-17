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
        }

        public virtual Type getUnitType()
        {
            return this.GetType();
        }
	}
}
