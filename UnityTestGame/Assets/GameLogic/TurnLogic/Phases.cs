using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameLogic.TurnLogic
{
	public enum Phases : int
	{
        Draw = 0,
        Main,
        BeginCombat,
        Declare,
        Move,
        React,
        Attack,
        EndCombat,
        Main2,
        End,
	}
}
