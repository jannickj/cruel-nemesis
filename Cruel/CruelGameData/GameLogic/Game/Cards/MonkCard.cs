﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CruelGameData.GameLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using XmasEngineModel.Management.Actions;
using CruelGameData.GameLogic.Game.Units;

namespace CruelGameData.GameLogic.Game.Cards
{
    public class MonkCard : SummoningSingleCard
	{
        public MonkCard()
        {
            this.RegisterModule(new GraphicsModule("monk"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Arcane, Mana.Arcane });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var target = (TerrainEntity)spell.Targets[0][0];
            var unit = new MonkUnit(this.Owner);
            spell.RunAction(new AddEntityAction(unit, target.Position.GenerateSpawn()));
            
        }
	}
}
