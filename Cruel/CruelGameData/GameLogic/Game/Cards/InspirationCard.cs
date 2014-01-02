using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CruelGameData.GameLogic.Game.Modules;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;

namespace CruelGameData.GameLogic.Game.Cards
{
	public class InspirationCard : GameCardWithSpellEffect
	{
        public InspirationCard()
        {
            this.RegisterModule(new GraphicsModule("inspiration"));
            this.SetTargetCondition(0, _ => true);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Arcane, Mana.Arcane, Mana.Arcane });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            Owner.Draw(2);
        }
	}
}
