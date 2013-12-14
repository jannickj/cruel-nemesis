using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using XmasEngineModel.Management.Actions;
using Assets.UnityLogic.Game.Units;

namespace Assets.UnityLogic.Game.Cards
{
	class SerpentCard : GameCardWithSpellEffect
	{
        public SerpentCard()
        {
            this.RegisterModule(new GraphicsModule("serpent"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Arcane });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var target = (TerrainEntity)spell.Targets[0][0];
            var unit = new SerpentUnit(this.Owner);
            spell.RunAction(new AddEntityAction(unit, target.Position.GenerateSpawn()));
            
        }
	}
}
