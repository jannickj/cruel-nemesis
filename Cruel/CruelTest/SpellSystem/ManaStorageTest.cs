using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cruel.GameLogic;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.PlayerCommands;

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
            TurnManager turnManager = new TurnManager();
            this.Engine.AddActor(turnManager);
            Player[] players = generatePlayersAndStartGame(2);
            Phases currentPhase = turnManager.CurrentPhase;
            ManaStorage m = new ManaStorage();
            m.AddCrystal(Mana.Divine);
            m.AddCrystal(Mana.Divine);
            m.AddCrystal(Mana.Arcane);

            Assert.IsFalse(m.IsCharged(Mana.Divine, 0));
            Assert.IsFalse(m.IsCharged(Mana.Divine, 1));
            Assert.IsFalse(m.IsCharged(Mana.Arcane, 0));

            changeTurn(players);

            Assert.IsTrue(m.IsCharged(Mana.Divine, 0));
            Assert.IsTrue(m.IsCharged(Mana.Divine, 1));
            Assert.IsTrue(m.IsCharged(Mana.Arcane, 0));
        }

        [TestMethod]
        public void CastCard_PlayerChoosesValidCrystals_CrystalsDischargedAndSpellIsCast()
        {

        }

        [TestMethod]
        public void CastCard_PlayerChoosesInvalidCrystal_CrystalIsNotDischargedAndSpellIsNotPayedFor()
        {

        }

        private Player[] generatePlayersAndStartGame(int count)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < count; i++)
            {
                Player p = new Player();
                this.ActionManager.Queue(new PlayerJoinAction(p));
                players.Add(p);
            }
            this.ActionManager.Queue(new StartGameCommand());
            this.Engine.Update();
            return players.ToArray();
        }

        private void changeTurn(Player[] players)
        {
            //TODO: Change turn
            Engine.Update();
        }
    }
}
