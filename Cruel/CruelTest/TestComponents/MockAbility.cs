using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;

namespace UnityTestGameTest.TestComponents
{
    public class MockAbility : Ability
    {
        private Action OnFiring;

        public MockAbility(Action OnFiring)
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
