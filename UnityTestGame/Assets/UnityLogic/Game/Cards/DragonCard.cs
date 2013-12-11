using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;
using XmasEngineModel.Management.Actions;
using Assets.UnityLogic.Game.Unit;

namespace Assets.UnityLogic.Game.Cards
{
    public class DragonCard : GameCardWithSpellEffect
	{
        public DragonCard()
        {
            this.RegisterModule(new GraphicsModule("dragon"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Fury, Mana.Fury, Mana.Fury, Mana.Fury, Mana.Fury });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var target = (TerrainEntity)spell.Targets[0][0];
            var unit = new DragonUnit(this.Owner);
            spell.RunAction(new AddEntityAction(unit, target.Position.GenerateSpawn()));
            
        }
	}
}
