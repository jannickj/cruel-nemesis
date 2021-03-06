﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Units;
using XmasEngineModel.Management.Actions;

namespace Assets.UnityLogic.Game.Cards
{
    class GoblinPikerCard : SummoningSingleCard
	{
        public GoblinPikerCard()
        {
            this.RegisterModule(new GraphicsModule("goblin_piker"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Fury });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var target = (TerrainEntity)spell.Targets[0][0];
            var unit = new GoblinPikerUnit(this.Owner);
            spell.RunAction(new AddEntityAction(unit, target.Position.GenerateSpawn()));
            
        }
	}
}
