using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.Unit;

namespace Assets.UnityLogic.Game.Cards
{
	public class BloodwyrmSpawnCard : GameCard
	{
        public BloodwyrmSpawnCard()
        {
            this.RegisterModule(new GraphicsModule("bloodwyrm_spawn"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
        }
	}
}
