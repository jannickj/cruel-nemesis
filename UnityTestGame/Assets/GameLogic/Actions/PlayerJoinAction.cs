using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using XmasEngineModel.Management.Actions;

namespace Assets.GameLogic.Actions
{
	public class PlayerJoinAction : EnvironmentAction
	{
        private Player player;

        public PlayerJoinAction(Player player)
        {
            this.player = player;
        }

        protected override void Execute()
        {
            this.RunAction(new AddXmasObjectAction(player));
            this.EventManager.Raise(new PlayerJoinedEvent(player));
        }
    }
}
