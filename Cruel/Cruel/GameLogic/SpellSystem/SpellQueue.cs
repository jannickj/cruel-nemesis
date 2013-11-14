using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.SpellSystem
{
    public class SpellQueue : XmasUniversal
    {
        public IEnumerable<Ability> Unresolved { get; private set;  }
    }
}
