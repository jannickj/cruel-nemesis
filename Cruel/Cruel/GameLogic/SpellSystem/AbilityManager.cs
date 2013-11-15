using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.SpellSystem
{
    public class AbilityManager : XmasUniversal
    {
        private Stack<Ability> stack = new Stack<Ability>();
        public IEnumerable<Ability> Unresolved { get {return stack;} }

        public AbilityManager()
        {
            this.EventManager.Register(new Trigger<EnqueueAbilityEvent>(evt => stack.Push(evt.Ability)));
        }
    }
}
