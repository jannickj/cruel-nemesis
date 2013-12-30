using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.PlayerCommands
{
    public abstract class PlayerCommand : EnvironmentAction
    {
        public Player Player { get; private set; }
        public PlayerCommand(Player player)
        {
            this.Player = player;
        }

    }
}
