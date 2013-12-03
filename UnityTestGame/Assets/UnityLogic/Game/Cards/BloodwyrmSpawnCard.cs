using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.Unit;
using XmasEngineModel.Management.Actions;
using XmasEngineExtensions.TileExtension;
using UnityEngine;

namespace Assets.UnityLogic.Game.Cards
{
	public class BloodwyrmSpawnCard : GameCardWithSpellEffect
	{
        public BloodwyrmSpawnCard()
        {
            this.RegisterModule(new GraphicsModule("bloodwyrm_spawn"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var target = (TerrainEntity)spell.Targets[0][0];
            var unit = new GruntUnit(this.Owner);
            spell.RunAction(new AddEntityAction(unit, target.Position.GenerateSpawn()));
            
        }
    }
}
