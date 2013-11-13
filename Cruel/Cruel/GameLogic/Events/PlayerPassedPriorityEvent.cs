using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events
{
	public class PlayerPassedPriorityEvent : XmasEvent
	{
        private Player player;

        public PlayerPassedPriorityEvent(Player player)
        {
            this.player = player;
        }

        public Player Player
        {
            get { return player; }

        }
	}
}
