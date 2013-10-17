using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.Modules;

namespace Assets.GameLogic.Unit
{
	public class GruntUnit : UnitEntity
	{

        public GruntUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
        }
	}
}
