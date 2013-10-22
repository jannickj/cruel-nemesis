using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic.SpellSystem
{
	public class GameCard : XmasUniversal
	{

        public Spell ConstructSpell()
        {
            throw new NotImplementedException();
        }
        public void AddSpellAction(Action<Spell> spell)
        {

        }

        public void SetTargetCondition(int index, Predicate<XmasActor> cond)
        {

        }
	}
}
