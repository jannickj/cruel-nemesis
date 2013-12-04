using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Actions;

namespace Assets.UnityLogic.Game
{
	public class GameLevelRewards : LevelRewarder
	{
        private Mana reward;

        public GameLevelRewards(Mana reward) : base(GenerateXPInterval())
        {
            this.reward = reward;
            for (int i = 0; i < this.MaxLevel; i++)
                this.SetReward(i, lr => Rewards());
        }

        private void Rewards()
        {
            this.ActionManager.Queue(new PlayerGainManaCrystalAction(this.Owner, reward));
        }

        private static IEnumerable<int> GenerateXPInterval()
        {
            return new int[] { 0, 100, 300, 500, 800, 1000 };
        }
	}
}
