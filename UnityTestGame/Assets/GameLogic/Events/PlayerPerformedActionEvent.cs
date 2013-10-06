using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
    public class PlayerPerformedActionEvent : XmasEvent
	{
        private Player player;

        public PlayerPerformedActionEvent(Player player)
        {
            this.player= player;
        }


        public Player Player { get; set; }
    }
}
