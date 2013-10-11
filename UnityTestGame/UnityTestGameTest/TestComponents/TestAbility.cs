using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.SpellSystem;
using Assets.GameLogic.Events;
using XmasEngineModel.Management;

namespace UnityTestGameTest.TestComponents
{
    public class TestAbility : Ability
    {
        private Action OnFiring;

        public TestAbility(Action OnFiring)
        {
            this.OnFiring = OnFiring;
            //this.EventManager.Register(new Trigger<AbilityTargetInvalidEvent>(_ => targetBecomesInvalid = true));
        }

        protected override void FireAbility()
        {
            OnFiring();
            this.EventManager.Raise(new AbilityResolvesEvent());
        }
    }
}
