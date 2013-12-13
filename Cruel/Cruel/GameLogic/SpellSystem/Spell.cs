using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.SpellSystem
{
	public class Spell : Ability
	{
        private List<Action<Spell>> effects = new List<Action<Spell>>();

        public Spell(Player controller, List<Action<Spell>> effects) : base(controller)
        {
            this.effects = effects;
        }
        
        protected override void FireAbility()
        {
            foreach (Action<Spell> s in effects)
            {
                s(this);
            }
        }

        public GameCard Creator { get; internal set; }
    }
}
