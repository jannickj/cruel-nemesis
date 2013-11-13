using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Modules;

namespace Cruel.GameLogic.Unit
{
	public class GruntUnit : UnitEntity
	{

        public GruntUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
        }
	}
}
