using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events
{
	public class PlayerAllowedToDeclareMoveAttackEvent : XmasEvent
	{

        public Player Player { get; private set; }

        public bool Allowed { get; private set; }

        public PlayerAllowedToDeclareMoveAttackEvent(GameLogic.Player player, bool allowed)
        {
            // TODO: Complete member initialization
            this.Player = player;
            this.Allowed = allowed;
        }
    }
}
