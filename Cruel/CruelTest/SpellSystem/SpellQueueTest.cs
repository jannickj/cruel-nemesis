using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CruelTest.TestComponents;
using CruelTest;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.PlayerCommands;
using Cruel.GameLogic;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.Actions;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class SpellQueueTest : EngineTest
    {
        private SpellQueue SpellQueue { get; set; }
        private TurnManager TurnManager { get; set; }

        public SpellQueueTest()
        {
            SpellQueue = new SpellQueue();
            this.Engine.AddActor(SpellQueue);
            TurnManager = new TurnManager();
            this.Engine.AddActor(TurnManager);
        }

        [TestMethod]
        public void QueueSpell_PlayerCastCard_SpellFromCardQueuedToStack()
        {
            MockCard card = new MockCard();
            Player castingPlayer = new Player();

            this.ActionManager.Queue(new CastCardCommand(castingPlayer, card));
            this.Engine.Update();

            IEnumerable<Ability> unresolved = SpellQueue.Unresolved;
            int expectedCount = 1;
            int actualCount = unresolved.Count();
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void ResolveSpell_TwoSpellsTwoPlayersEachPassPriority_StackIsEmptyedAndPhaseIsSame()
        {
            bool mustResolve = false;
            bool mustResolveAswell = false;
            Player[] players = generatePlayersAndStartGame(2);
            Phases currentPhase = this.TurnManager.CurrentPhase;

            MockCard card1 = new MockCard();
            card1.AddSpellAction(_ => mustResolveAswell = true);
            MockCard card2 = new MockCard();
            card2.AddSpellAction(_ => mustResolve = true);
            
            this.ActionManager.Queue(new CastCardCommand(players[0], card1));
            this.ActionManager.Queue(new CastCardCommand(players[0], card2));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[1]));
            this.Engine.Update();

            Phases actualPhase = this.TurnManager.CurrentPhase;

            Assert.AreEqual(currentPhase, actualPhase);
            Assert.IsTrue(mustResolve);
            Assert.IsTrue(mustResolveAswell);
        }

        [TestMethod]
        public void QueueSpell_LastPlayerCastSpell_FirstPlayerGainsPriorityAndNoSpellsAreResolved()
        {
            bool spellResolved = false;
            Player[] players = generatePlayersAndStartGame(2);
            Phases currentPhase = this.TurnManager.CurrentPhase;
            MockCard card = new MockCard();
            card.AddSpellAction(_ => spellResolved = true);

            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            
            this.ActionManager.Queue(new CastCardCommand(players[1], card));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[1]));
            this.Engine.Update();


        }

        [TestMethod]
        public void SpellResolving_SpellTriggersAnAbilityAndGetPutOnQueue_StackIsEmptyBeforePriorityIsGiven()
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

        

        
    }
}
