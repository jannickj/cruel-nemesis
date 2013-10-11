using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityTestGameTest.TestComponents;
using Assets.GameLogic.SpellSystem;

namespace UnityTestGameTest.SpellSystem
{
    [TestClass]
    public class CardTest : EngineTest
    {
        [TestMethod]
        public void ConstructSpell_SimpleSpell_CorrectlyConstructed()
        {
            bool spellResolved = false;
            TestCard card = new TestCard();
            card.SetTargetCondition(0, _ => true);
            card.AddSpellAction(_ => spellResolved = true);

            Spell spell = card.ConstructSpell();

            spell.FireAbility();


            Assert.IsTrue(spellResolved);
        }
    }
}
