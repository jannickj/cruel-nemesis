using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;

namespace Cruel.GameLogic.PlayerCommands
{
	public class PlayerPassPriorityCommand : PlayerCommand
	{

        public PlayerPassPriorityCommand(Player player) : base(player)
        {
            
        }


        protected override void Execute()
        {
            this.EventManager.Raise(new PlayerPassedPriorityEvent(Player));
        }
    }
}
