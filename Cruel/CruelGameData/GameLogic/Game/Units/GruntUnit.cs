using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Modules;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic;

namespace CruelGameData.GameLogic.Game.Unit
{
	public class GruntUnit : UnitEntity
	{

        public GruntUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
        }
	}
}
