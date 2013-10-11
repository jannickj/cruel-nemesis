using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityTestGameTest.TestComponents;
using Assets.GameLogic.SpellSystem;
using Assets.GameLogic.Unit;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using XmasEngineModel;

namespace UnityTestGameTest.SpellSystem
{
    [TestClass]
    public class AbilityTest
    {
        private EventManager evtman = new EventManager();

        public AbilityTest()
        {
            
        }

        [TestMethod]
        public void FireAbility_HasValidTarget_Resolves()
        {
            bool hasfired = false;
            bool resolvesEventTriggered = false;

            Ability abi = new TestAbility(() => hasfired = true);
            abi.EventManager = evtman;

            evtman.Register(new Trigger<AbilityResolvesEvent>(_ => resolvesEventTriggered = true));

            UnitEntity unit = new TestUnit();
            abi.SetTarget(0, new XmasActor[]{unit});

            abi.FireAbility();

            Assert.IsTrue(hasfired);
            Assert.IsTrue(resolvesEventTriggered);

        }



        [TestMethod]
        public void FireAbility_TargetIsRemovedFromGame_DoesNotResolve()
        {
            bool hasfired = false;
            bool resolvesEventTriggered = false;
            bool targetBecomesInvalid = true;

            Ability abi = new TestAbility(() => hasfired = true);
            abi.EventManager = evtman;

            evtman.Register(new Trigger<AbilityResolvesEvent>(_ => resolvesEventTriggered = true));
            evtman.Register(new Trigger<AbilityTargetInvalidEvent>(_ => targetBecomesInvalid = true));

            UnitEntity unit = new TestUnit();
            unit.EventManager = evtman;
            abi.SetTarget(0, new XmasActor[]{unit});

            unit.Raise(new RemovedFromGameEvent());

            abi.FireAbility();

            Assert.IsFalse(hasfired);
            Assert.IsFalse(resolvesEventTriggered);
            Assert.IsTrue(targetBecomesInvalid);

        }

        [TestMethod]
        public void FireAbility_TargetConditionFalse_DoesNotResolve()
        {
            bool hasfired = false;
            bool resolvesEventTriggered = false;
            bool targetBecomesInvalid = true;

            Ability abi = new TestAbility(() => hasfired = true);
            abi.EventManager = evtman;
            

            evtman.Register(new Trigger<AbilityResolvesEvent>(_ => resolvesEventTriggered = true));
            evtman.Register(new Trigger<AbilityTargetInvalidEvent>(_ => targetBecomesInvalid = true));

            UnitEntity unit = new TestUnit();
            abi.SetTarget(0, new XmasActor[]{unit});

            abi.SetTargetCondition(0, _ => false);

            

            abi.FireAbility();


            
            Assert.IsFalse(hasfired);
            Assert.IsFalse(resolvesEventTriggered);
            Assert.IsTrue(targetBecomesInvalid);

        }
    }
}
