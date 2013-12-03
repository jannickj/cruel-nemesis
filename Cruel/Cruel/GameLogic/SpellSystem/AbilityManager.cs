using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;
using Cruel.GameLogic.Actions;

namespace Cruel.GameLogic.SpellSystem
{
    public class AbilityManager : XmasUniversal
    {
        private Stack<Ability> stack = new Stack<Ability>();
        public IEnumerable<Ability> Unresolved { get {return stack;} }

        protected override void OnAddedToEngine()
        {
            this.EventManager.Register(new Trigger<EnqueueAbilityEvent>(OnEnqueueAbility));
            this.EventManager.Register(new Trigger<PhaseChangingEvent>(OnPhaseChange));
        }

        private void OnEnqueueAbility(EnqueueAbilityEvent evt)
        {
            stack.Push(evt.Ability);
           
        }
        
        private void OnPhaseChange(PhaseChangingEvent evt)
        {
            if (StackNotEmpty())
            {
                evt.SetPreventPhase();
                while (StackNotEmpty())
                {
                    var abi = stack.Pop();
                    if(!abi.PreventResolving)
                        this.ActionManager.Queue(new FireAbilityAction(abi));

                    this.EventManager.Raise(new AbilityRemovedFromStackEvent(abi));
                }
            }
        }

        private bool StackNotEmpty() { return stack.Count != 0; }
    }
}
