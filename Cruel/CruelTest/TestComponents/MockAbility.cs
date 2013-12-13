using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;

namespace CruelTest.TestComponents
{
    public class MockAbility : Ability
    {
        private Action OnFiring;

        public MockAbility(Action OnFiring) : base(null)
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
