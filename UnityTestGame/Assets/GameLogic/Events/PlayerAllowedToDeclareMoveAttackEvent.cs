using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
	public class PlayerAllowedToDeclareMoveAttackEvent : XmasEvent
	{
        private bool p;

        public Player Player { get; set; }

        public bool Allowed { get; set; }

        public PlayerAllowedToDeclareMoveAttackEvent(GameLogic.Player player)
        {
            // TODO: Complete member initialization
            this.Player = player;
        }

        public PlayerAllowedToDeclareMoveAttackEvent(GameLogic.Player player, bool p)
        {
            // TODO: Complete member initialization
            this.Player = player;
            this.p = p;
        }
    }
}
