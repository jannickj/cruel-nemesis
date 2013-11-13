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

        public Spell(List<Action<Spell>> effects)
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
    }
}
