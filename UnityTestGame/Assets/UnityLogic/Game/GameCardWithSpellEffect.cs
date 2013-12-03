using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Game
{
	public abstract class GameCardWithSpellEffect : GameCard
	{
        public GameCardWithSpellEffect()
        {
            this.AddSpellAction(OnSpellEffect);
        }

        protected abstract void OnSpellEffect(Spell spell);
	}
}
