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
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class AbilityManagerTest : EngineTest
    {
        private AbilityManager AbilityManager { get; set; }
        private TurnManager TurnManager { get; set; }

        public AbilityManagerTest()
        {
            AbilityManager = new AbilityManager();
            this.Engine.AddActor(AbilityManager);
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

            IEnumerable<Ability> unresolved = AbilityManager.Unresolved;
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
        public void QueueSpell_FirstPlayersTurnOtherPlayerCastSpell_FirstPlayerGetsPriorityFirst()
        {
            Player[] players = generatePlayersAndStartGame(2);
            Phases currentPhase = this.TurnManager.CurrentPhase;
            MockCard card = new MockCard();
            
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            this.Engine.Update();
            bool firstPlayerGainPrioOnCast = false;
            bool secondPlayerGainPrioOnCast = false;
            this.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => firstPlayerGainPrioOnCast = players[0] == evt.Player));
            this.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => secondPlayerGainPrioOnCast = players[1] == evt.Player));
            this.ActionManager.Queue(new CastCardCommand(players[1], card));
            this.Engine.Update();

            Assert.IsTrue(firstPlayerGainPrioOnCast);
            Assert.IsFalse(secondPlayerGainPrioOnCast);


        }

        [TestMethod]
        public void SpellResolving_SpellTriggersAnAbilityAndGetPutOnQueue_StackIsEmptyBeforePriorityIsGiven()
        {
            bool correctPlayerHasPriority = false;
            Player[] players = generatePlayersAndStartGame(2);
            Phases currentPhase = this.TurnManager.CurrentPhase;
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            this.Engine.Update();
            MockAbility triggeredAbility = new MockAbility(() => { });
            triggeredAbility.Resolved += (_, _2) => correctPlayerHasPriority = (TurnManager.PlayerWithPriority == players[1]);
            MockCard card = new MockCard();
            card.AddSpellAction(_ => this.ActionManager.Queue(new FireAbilityAction(triggeredAbility)));
            this.ActionManager.Queue(new CastCardCommand(players[1], card));
            this.Engine.Update();
            this.Engine.Update();
            Assert.IsTrue(correctPlayerHasPriority);
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
