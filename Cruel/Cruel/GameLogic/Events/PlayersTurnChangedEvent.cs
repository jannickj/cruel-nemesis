using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events
{
	public class PlayersTurnChangedEvent : XmasEvent
	{
        private Player playersTurn;

        public Player PlayersTurn
        {
            get { return playersTurn; }
        }

        public PlayersTurnChangedEvent(Player playersTurn)
        {
            // TODO: Complete member initialization
            this.playersTurn = playersTurn;
        }
	}
}
