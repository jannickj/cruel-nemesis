using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
	public class PlayerJoinedEvent : XmasEvent
	{
        private Player player;

        public PlayerJoinedEvent(Player player)
        {
            this.player = player;
        }

        public Player Player
        {
            get { return player; }

        }

	}
}
