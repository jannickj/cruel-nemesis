using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;

namespace Cruel.GameLogic
{
    public class LevelRewarder : XmasUniversal
    {
        public Player Owner { get; internal set; }
        public int CurrentLevel { get; private set; }
        private int[] xpIntervals;
        public Dictionary<int, Action<LevelRewarder>> rewards = new Dictionary<int, Action<LevelRewarder>>();
        public int MaxLevel { get; private set; }

        public LevelRewarder(IEnumerable<int> xpIntervals)
        {
            this.CurrentLevel = 0;
            this.xpIntervals = xpIntervals.ToArray();
            this.MaxLevel = this.xpIntervals.Length;
        }

        protected override void OnAddedToEngine()
        {
            CheckLevelUp();
            Owner.Register(new Trigger<PlayerGainedXPEvent>(OnPlayerGainXP));
        }

        public void SetReward(int level, Action<LevelRewarder> reward)
        {
            this.rewards[level] = reward;
        }

        private void OnPlayerGainXP(PlayerGainedXPEvent evt)
        {
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            int levelsGained = 0;
            int checklevel = CurrentLevel + 1;
            while (true)
            {
                
                if (xpIntervals.Length >= checklevel)
                {
                    int levelIndex = checklevel - 1;
                    int xprequired = this.xpIntervals[levelIndex];
                    if (this.Owner.CurrentXP >= xprequired)
                    {
                        levelsGained++;
                    }
                    else
                        break;
                }
                else
                    break;

                checklevel++;

            }

            for (int i = 0; i < levelsGained; i++)
            {
                this.CurrentLevel++;
                Action<LevelRewarder> reward;
                if (this.rewards.TryGetValue(this.CurrentLevel, out reward))
                    reward(this);
            }
        }
    }
}
