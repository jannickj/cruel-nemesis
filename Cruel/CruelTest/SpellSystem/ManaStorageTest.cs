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
using CruelTest.TestComponents;
using Cruel.GameLogic.Events;

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

            ManaStorage m = new ManaStorage();
            this.Engine.AddActor(m);
            m.Owner = players[1];
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
            bool spellResolved = false;

            ManaStorage m = new ManaStorage();
            m.AddCrystal(Mana.Divine);
            m.AddCrystal(Mana.Divine);
            m.AddCrystal(Mana.Arcane);
            m.chargeAll();

            List<Mana> manaCost = new List<Mana>();
            manaCost.Add(Mana.Divine);
            manaCost.Add(Mana.Arcane);

            MockCard card = new MockCard();
            card.ManaCost = manaCost;
            card.AddSpellAction(_ => spellResolved = true);

            Player[] players = generatePlayersAndStartGame(2);
            Player owner = players[0];
            m.Owner = owner;

            List<Mana> selectedMana = new List<Mana>();
            selectedMana.Add(Mana.Divine);
            selectedMana.Add(Mana.Arcane);

            this.ActionManager.Queue(new CastCardCommand(owner, card, selectedMana));
            this.Engine.Update();

            Assert.IsFalse(m.IsCharged(Mana.Divine, 0));
            Assert.IsTrue(m.IsCharged(Mana.Divine, 1));
            Assert.IsFalse(m.IsCharged(Mana.Arcane, 0));

            Assert.IsTrue(spellResolved);
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
            for (int i = 0; i < (int)Phases.End; i++)
            {
                EventManager.Raise(new PlayerPassedPriorityEvent(players[0]));
                EventManager.Raise(new PlayerPassedPriorityEvent(players[1]));
                Engine.Update();
            }
        }
    }
}
