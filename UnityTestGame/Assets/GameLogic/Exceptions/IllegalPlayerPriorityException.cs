using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameLogic.Exceptions
{
    public enum IllegalPriorityActions
    {
        Passed_Priority,
        Performed_Action
    }

	public class IllegalPlayerPriorityException : Exception
	{
        private IllegalPriorityActions action;
        private Player player;

        public Player Player
        {
            get { return player; }
        }


        public IllegalPriorityActions Action
        {
            get { return action; }
        }

        public IllegalPlayerPriorityException(IllegalPriorityActions action, Player p) : base("Player: "+p+" "+action.ToString()+" without priority")
        {
            this.action = action;
            this.player = p;
        }
	}
}
