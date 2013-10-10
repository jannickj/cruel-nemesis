using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic;
using Assets.GameLogic.Events;
using Assets.GameLogic.Actions;

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
            this.ActionManager.Queue(new PlayerPassPriorityAction(player));
            this.Finished = true;
        }
    }
}
