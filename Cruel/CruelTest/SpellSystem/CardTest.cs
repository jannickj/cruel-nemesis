using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CruelTest.TestComponents;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class CardTest : EngineTest
    {
        [TestMethod]
        public void ConstructSpell_SimpleSpell_CorrectlyConstructed()
        {
            bool spellResolved = false;
            MockCard card = new MockCard();
            card.SetTargetCondition(0, _ => true);
            
            card.AddSpellAction(_ => spellResolved = true);

            Spell spell = card.ConstructSpell();
            spell.SetTarget(0, new object[]{null});

            this.ActionManager.Queue(spell);
            this.ActionManager.ExecuteActions();


            Assert.IsTrue(spellResolved);
        }

        [TestMethod]
        public void TestTarget_ValidTarget_TargetAccepted()
        {
            int firstTarget = 0;
            MockCard card = new MockCard();
            MockUnit someUnit = new MockUnit();

            card.SetTargetCondition(firstTarget,unit => unit is UnitEntity);
            bool result = card.TestTarget(firstTarget, someUnit);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestTarget_NoTargetCondition_TargetAccepted()
        {
            int firstTarget = 0;
            MockCard card = new MockCard();
            MockUnit someUnit = new MockUnit();

            bool result = card.TestTarget(firstTarget, someUnit);

            Assert.IsTrue(result);
        }
    }
}
