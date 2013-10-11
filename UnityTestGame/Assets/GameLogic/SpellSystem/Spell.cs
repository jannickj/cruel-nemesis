using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;

namespace Assets.GameLogic.SpellSystem
{
	public class Spell : Ability
	{
        public TActor GetTargetAs<TActor>(int p) where TActor : XmasActor
        {
            throw new NotImplementedException();
        }
    }
}
