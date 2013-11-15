using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Actions
{
    public class FireAbilityAction : EnvironmentAction
    {
        private Ability ability;

        public FireAbilityAction(Ability a)
        {
            this.ability = a;
        }

        protected override void Execute()
        {
            this.RunAction(ability);
        }
    }
}
