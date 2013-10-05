using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic.Unit
{
	public abstract class UnitEntity : XmasEntity 
	{

        public virtual Type getUnitType()
        {
            return this.GetType();
        }
	}
}
