using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;

namespace Cruel.GameLogic.PlayerCommands
{
	public class PlayerPassPriorityCommand : EnvironmentAction
	{
        private Player player;

        public PlayerPassPriorityCommand(Player player)
        {
            this.player = player;
        }


        protected override void Execute()
        {
            this.EventManager.Raise(new PlayerPassedPriorityEvent(player));
        }
    }
}
