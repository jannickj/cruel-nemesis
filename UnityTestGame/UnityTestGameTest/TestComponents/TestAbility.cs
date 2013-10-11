using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.SpellSystem;

namespace UnityTestGameTest.TestComponents
{
    public class TestAbility : Ability
    {
        public TestAbility(Action OnFiring)
        {

        }

        public override void FireAbility()
        {
            throw new NotImplementedException();
        }
    }
}
