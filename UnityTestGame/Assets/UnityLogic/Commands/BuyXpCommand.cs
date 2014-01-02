using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.PlayerCommands;
using CruelGameData.GameLogic.Game;

namespace Assets.UnityLogic.Commands
{
	public class BuyXpCommand : Command
	{
        public override void Update()
        {

            var player = this.GuiController.GuiInfo.Player;
            var PlayerRewarder = (GameLevelRewards)player.Rewarder;
            var MainMana = PlayerRewarder.MainManaType;
            this.ActionManager.Queue(new GainXPCommand(player, 100, MainMana));
            this.Finished = true;

        }
    }
}
