using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Actions
{
    public class DrawCardAction : EnvironmentAction
    {
        private Player player;
        private int amount;

        public DrawCardAction(Player player, int amount)
        {
            this.player = player;
            this.amount = amount;
        }


        protected override void Execute()
        {
            player.Draw(amount);
        }
    }
}
