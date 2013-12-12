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
        private List<Mana> mana = new List<Mana>();

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

            this.ActionManager.Queue(new CastCardCommand(castingPlayer, card, mana));
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
            
            this.ActionManager.Queue(new CastCardCommand(players[0], card1, mana));
            this.ActionManager.Queue(new CastCardCommand(players[0], card2, mana));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[1]));
            this.Engine.Update();

            Phases actualPhase = this.TurnManager.CurrentPhase;

            Assert.AreEqual(currentPhase, actualPhase);
            Assert.IsTrue(mustResolve);
            Assert.IsTrue(mustResolveAswell);
        }

        [TestMethod]
        public void ResolveStack_StackIsEmptiedAndBothPlayersPass_PhaseIsChanged()
        {
            bool mustResolve = false;
            bool mustResolveAswell = false;
            Player[] players = generatePlayersAndStartGame(2);
            Phases currentPhase = this.TurnManager.CurrentPhase;

            MockCard card1 = new MockCard();
            card1.AddSpellAction(_ => mustResolveAswell = true);
            MockCard card2 = new MockCard();
            card2.AddSpellAction(_ => mustResolve = true);

            this.ActionManager.Queue(new CastCardCommand(players[0], card1, mana));
            this.ActionManager.Queue(new CastCardCommand(players[0], card2, mana));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[1]));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[0]));
            this.ActionManager.Queue(new PlayerPassPriorityCommand(players[1]));
            this.Engine.Update();

            Phases actualPhase = this.TurnManager.CurrentPhase;

            Assert.AreNotEqual(currentPhase, actualPhase);
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
            this.ActionManager.Queue(new CastCardCommand(players[1], card, mana));
            this.Engine.Update();

            Assert.IsTrue(firstPlayerGainPrioOnCast);
            Assert.IsFalse(secondPlayerGainPrioOnCast);


        }

        [TestMethod]
        public void InjectedAbilities_TwoAbilitiesFirstAbilityTriggerAnotherAbility_AbilityIsResolvedBeforeLastAbility()
        {
            //Given that two spells are on the stack, if one of those spells trigger something then the triggered ability must be resolved before the last spell
            //For instance if a player summons a creature and another kill something else in responds
            //The death of that creature might trigger something that prevents the creature from being summoned.

            bool secondAbilityFired = false;
            bool isSecondAbilityFired = false;
            MockAbility injectedAbility = new MockAbility(() => isSecondAbilityFired = secondAbilityFired);
            MockAbility FirstAbility = new MockAbility(() => this.EventManager.Raise(new EnqueueAbilityEvent(injectedAbility)));
            MockAbility SecondAbility = new MockAbility(() => secondAbilityFired = true);

            this.EventManager.Raise(new EnqueueAbilityEvent(SecondAbility));
            this.EventManager.Raise(new EnqueueAbilityEvent(FirstAbility));
            this.EventManager.Raise(new PhaseChangedEvent(Phases.Draw,Phases.Main));
            this.Engine.Update();
            this.Engine.Update();

            Assert.IsFalse(isSecondAbilityFired);

        }

        [TestMethod]
        public void InjectedAbilities_AnAbilityInjectsANewAbility_InjectedAbilityIsResolvedWithoutPhaseChange()
        {
            

            bool isInjectedResolved = false;
            MockAbility injectedAbility = new MockAbility(() => isInjectedResolved = true);
            MockAbility FirstAbility = new MockAbility(() => this.EventManager.Raise(new EnqueueAbilityEvent(injectedAbility)));

            this.EventManager.Raise(new EnqueueAbilityEvent(FirstAbility));
            this.EventManager.Raise(new PhaseChangedEvent(Phases.Draw, Phases.Main));
            this.Engine.Update();
            this.Engine.Update();

            Assert.IsTrue(isInjectedResolved);

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
