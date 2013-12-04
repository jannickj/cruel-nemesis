using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cruel.GameLogic;

namespace CruelTest
{
    [TestClass]
    public class LevelRewarderTest : EngineTest
    {
        [TestMethod]
        public void GainLevel_ZeroXPRequriedForLevelUp_RewardGiven()
        {
            bool rewardGiven = false;
            int[] xplevels = new int[]{0};
            LevelRewarder levelReward = new LevelRewarder(xplevels);
            Player player = new Player(null, null, null, levelReward);
            levelReward.SetReward(1,rw => rewardGiven = true);
            this.Engine.AddActor(player);
            this.Engine.Update();

            Assert.IsTrue(rewardGiven);
        }

        [TestMethod]
        public void GainLevel_HasAReward_RewardGiven()
        {
            bool rewardGiven = false;
            int[] xplevels = new int[] { 0, 100 };
            LevelRewarder levelReward = new LevelRewarder(xplevels);
            Player player = new Player(null, null, null, levelReward);
            levelReward.SetReward(2, rw => rewardGiven = true);
            this.Engine.AddActor(player);
            this.Engine.Update();
            player.AddXP(100);

            Assert.IsTrue(rewardGiven);
        }

        [TestMethod]
        public void GainLevel_GetsXpForMultipleLevels_RewardsForAllLevelsGiven()
        {
            bool FirstRewardGiven = false;
            bool SecondRewardGiven = false;
            int[] xplevels = new int[] { 0, 100, 200 };
            LevelRewarder levelReward = new LevelRewarder(xplevels);
            Player player = new Player(null, null, null, levelReward);
            levelReward.SetReward(2, rw => FirstRewardGiven = true);
            levelReward.SetReward(3, rw => SecondRewardGiven = true);
            this.Engine.AddActor(player);
            this.Engine.Update();
            player.AddXP(200);

            Assert.IsTrue(FirstRewardGiven);
            Assert.IsTrue(SecondRewardGiven);
        }

        [TestMethod]
        public void GainLevel_HasNoRewardForLevel_NoRewardsGiven()
        {
            int[] xplevels = new int[] { 0, 100 };
            LevelRewarder levelReward = new LevelRewarder(xplevels);
            Player player = new Player(null, null, null, levelReward);
            this.Engine.AddActor(player);
            this.Engine.Update();
            player.AddXP(100);

            Assert.AreEqual(2, levelReward.CurrentLevel);
        }
    }
}
