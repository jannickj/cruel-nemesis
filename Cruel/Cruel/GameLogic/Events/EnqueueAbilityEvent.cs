using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Events
{
    public class EnqueueAbilityEvent : XmasEvent
    {
        private Ability ability;

        public Ability Ability
        {
            get { return ability; }
            set { ability = value; }
        }

        public EnqueueAbilityEvent(Ability a)
        {
            ability = a;
        }
    }
}
