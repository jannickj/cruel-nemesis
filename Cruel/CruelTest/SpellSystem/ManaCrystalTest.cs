using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cruel.GameLogic.SpellSystem;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class ManaCrystalTest : EngineTest
    {
        [TestMethod]
        public void SpendMana_ManaCrystalIsChargedAndUsed_CrystalIsChargedThenLosesChargeAndReturnsCorrectType()
        {
            ManaCrystal m = new ManaCrystal(Mana.Divine);
            m.Charge();
            Assert.IsTrue(m.IsCharged);
            Mana type = m.Spend();
            Assert.IsTrue(type == Mana.Divine);
            Assert.IsFalse(m.IsCharged);
        }
    }
}
