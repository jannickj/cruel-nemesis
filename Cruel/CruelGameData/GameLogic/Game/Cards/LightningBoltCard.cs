using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CruelGameData.GameLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;
using XmasEngineModel.Management.Actions;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.Modules;

namespace CruelGameData.GameLogic.Game.Cards
{
	public class LightningBoltCard : GameCardWithSpellEffect
	{
        public LightningBoltCard()
        {
            this.RegisterModule(new GraphicsModule("lightningbolt"));
            this.SetTargetCondition(0, obj => obj is UnitEntity && !((UnitEntity)obj).Module<HealthModule>().IsDead());
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Arcane });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var target = (UnitEntity)spell.Targets[0][0];
            spell.RunAction(target,new DealDamageToUnitAction(target, 2));
            
        }
	}
}
