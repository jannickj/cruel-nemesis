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
            this.Engine.AddActor(m);
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
            this.FailTestOnEngineCrash();
            bool spellResolved = false;

            AbilityManager am = new AbilityManager();
            this.Engine.AddActor(am);
            TurnManager tm = new TurnManager();
            this.Engine.AddActor(tm);

            ManaStorage m = new ManaStorage();
            Player[] players = generatePlayersAndStartGame(new Player[]{new Player(null,null,m,null),new Player()});
            this.Engine.Update();
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


            
            Player owner = players[0];
            

            List<Mana> selectedMana = new List<Mana>();
            selectedMana.Add(Mana.Divine);
            selectedMana.Add(Mana.Arcane);

            this.ActionManager.Queue(new CastCardCommand(owner, card, selectedMana));
            this.Engine.Update();
            EventManager.Raise(new PlayerPassedPriorityEvent(players[0]));
            EventManager.Raise(new PlayerPassedPriorityEvent(players[1]));
            this.Engine.Update();

            Assert.IsTrue(m.IsCharged(Mana.Divine, 0));
            Assert.IsFalse(m.IsCharged(Mana.Divine, 1));
            Assert.IsFalse(m.IsCharged(Mana.Arcane, 0));

            Assert.IsTrue(spellResolved);
        }

        private Player[] generatePlayersAndStartGame(IEnumerable<Player> players)
        {

            foreach(var p in players)
            {
                this.ActionManager.Queue(new PlayerJoinAction(p));
            }
            this.ActionManager.Queue(new StartGameCommand());
            this.Engine.Update();
            return players.ToArray();
        }

        private Player[] generatePlayersAndStartGame(int count)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < count; i++)
            {
                players.Add(new Player());
            }
            return generatePlayersAndStartGame(players);
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
