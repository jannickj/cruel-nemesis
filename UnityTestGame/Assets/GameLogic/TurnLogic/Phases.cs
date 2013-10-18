using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameLogic.TurnLogic
{
	public enum Phases
	{
        Draw = 0,
        Main = 1,
        BeginCombat = 2,
        Declare = 3,
        Move = 4,
        Attack = 5,
        Main2 = 6,
        End = 7
	}
}
