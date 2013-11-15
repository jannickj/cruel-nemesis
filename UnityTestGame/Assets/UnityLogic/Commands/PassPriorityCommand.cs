using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.PlayerCommands;

namespace Assets.UnityLogic.Commands
{
	public class PassPriorityCommand : Command
	{
        private Player player;

        public PassPriorityCommand(Player player)
        {
            this.player = player;
        }
        public override void Update()
        {
            this.ActionManager.Queue(new PlayerPassPriorityCommand(player));
            this.Finished = true;
        }
    }
}
