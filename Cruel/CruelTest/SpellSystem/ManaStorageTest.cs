using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cruel.GameLogic;
using Cruel.GameLogic.SpellSystem;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class ManaStorageTest : EngineTest
    {
        [TestMethod]
        public void AddCrystal_NoCrystals_OneCrystalAddedWithoutCharge()
        {
            ManaStorage m = new ManaStorage();
            m.AddCrystal(Mana.Divine);
            Assert.IsFalse(m.IsCharged(Mana.Divine, 0));
        }

        [TestMethod]
        public void NewTurn_TurnEndsAndANewTurnBegins_AllCrystalsAreRecharged()
        {

        }

        [TestMethod]
        public void CastCard_PlayerChoosesValidCrystals_CrystalsDischargedAndSpellIsCast()
        {

        }

        [TestMethod]
        public void CastCard_PlayerChoosesInvalidCrystal_CrystalIsNotDischargedAndSpellIsNotPayedFor()
        {

        }
    }
}
