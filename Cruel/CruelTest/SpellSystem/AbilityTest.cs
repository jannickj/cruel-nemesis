using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CruelTest.TestComponents;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class AbilityTest : EngineTest
    {
        public AbilityTest()
        {
            
        }

        [TestMethod]
        public void FireAbility_HasValidTarget_Resolves()
        {
            bool hasfired = false;
            bool resolvesEventTriggered = false;

            Ability abi = new MockAbility(() => hasfired = true);
            abi.EventManager = this.EventManager;

            this.EventManager.Register(new Trigger<AbilityResolvesEvent>(_ => resolvesEventTriggered = true));

            UnitEntity unit = new MockUnit();
            abi.SetTarget(0, new XmasUniversal[] { unit });

            this.ActionManager.Queue(abi);
            this.ActionManager.ExecuteActions();

            Assert.IsTrue(hasfired);
            Assert.IsTrue(resolvesEventTriggered);

        }



        [TestMethod]
        public void FireAbility_TargetIsRemovedFromGame_DoesNotResolve()
        {
            bool hasfired = false;
            bool resolvesEventTriggered = false;
            bool targetBecomesInvalid = false;

            Ability abi = new MockAbility(() => hasfired = true);
            abi.EventManager = this.EventManager;

            this.EventManager.Register(new Trigger<AbilityResolvesEvent>(_ => resolvesEventTriggered = true));
            this.EventManager.Register(new Trigger<AbilityTargetInvalidEvent>(_ => targetBecomesInvalid = true));

            UnitEntity unit = new MockUnit();
            unit.EventManager = this.EventManager;
            abi.SetTarget(0, new XmasUniversal[] { unit });

            unit.Raise(new RemovedFromGameEvent());

            this.ActionManager.Queue(abi);
            this.ActionManager.ExecuteActions();

            Assert.IsFalse(hasfired);
            Assert.IsFalse(resolvesEventTriggered);
            Assert.IsTrue(targetBecomesInvalid);

        }

        [TestMethod]
        public void Targets_HasOneOnFirstTargetZeroOnSecondTargetTwoOnThirdTarget_ReturnsCorrectTargets()
        {
            UnitEntity first = new MockUnit();

            UnitEntity Third1 = new MockUnit();
            UnitEntity Third2 = new MockUnit();
            Ability abi = new MockAbility(null);
            abi.SetTarget(0, new object[] { first });
            abi.SetTarget(2, new object[] { Third1, Third2 });

            var targets = abi.Targets;
            int secondcount = 0;

            Assert.AreEqual(first, targets[0][0]);
            Assert.AreEqual(secondcount, targets[1].Length);
            Assert.AreEqual(Third1, targets[2][0]);
            Assert.AreEqual(Third2, targets[2][1]);
        }

        [TestMethod]
        public void FireAbility_TargetConditionFalse_DoesNotResolve()
        {
            bool hasfired = false;
            bool resolvesEventTriggered = false;
            bool targetBecomesInvalid = true;

            Ability abi = new MockAbility(() => hasfired = true);
            abi.EventManager = this.EventManager;
            

            this.EventManager.Register(new Trigger<AbilityResolvesEvent>(_ => resolvesEventTriggered = true));
            this.EventManager.Register(new Trigger<AbilityTargetInvalidEvent>(_ => targetBecomesInvalid = true));

            UnitEntity unit = new MockUnit();
            abi.SetTarget(0, new XmasUniversal[] { unit });

            abi.SetTargetCondition(0, _ => false);


            this.ActionManager.Queue(abi);
            this.ActionManager.ExecuteActions();


            
            Assert.IsFalse(hasfired);
            Assert.IsFalse(resolvesEventTriggered);
            Assert.IsTrue(targetBecomesInvalid);

        }
    }
}
