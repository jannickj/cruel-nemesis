using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GameLogic.TurnLogic;
using Assets.GameLogic.Events;
using Assets.GameLogic;
using XmasEngineModel.Management;

namespace UnityTestGameTest.TurnLogic
{
    [TestClass]
    public class TurnManagerTest : EngineTest
    {
        private TurnManager turnman;

        public TurnManagerTest()
        {
            turnman = new TurnManager();
            Engine.AddActor(turnman);
            turnman.Initialize();
        }

        [TestMethod]
        public void GameStart_Player1sTurn_TurnPhaseAndPriorityCorrectlySet()
        {
            Player p1 = ConstructNAddPlayer();
            Player p2 = ConstructNAddPlayer();

            bool p1sturn = false;
            bool p1priority = false;
            bool correctPhase = false;

            EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => p1sturn = evt.PlayersTurn == p1));
            EventManager.Register(new Trigger<PhaseChangedEvent>(evt => correctPhase = evt.NewPhase == Phases.Draw));
            EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => p1priority = evt.Player == p1));

            EventManager.Raise(new GameStartEvent());

            Assert.IsTrue(p1sturn);
            Assert.IsTrue(p1priority);
            Assert.IsTrue(correctPhase);


        }


        [TestMethod]
        public void PassPriority_Player1PassPriority_Player2GainsPriority()
        {
            Player p1 = ConstructNAddPlayer();
            Player p2 = ConstructNAddPlayer();

            bool p2priority = false;

            EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => p2priority = evt.Player == p2));

            EventManager.Raise(new GameStartEvent());

            EventManager.Raise(new PlayerPassedPriorityEvent(p1));

            Assert.IsTrue(p2priority);
        }


        [TestMethod]
        public void PassPriority_Player2PassPriority_Player1GainsPriorityBackAndPhaseChange()
        {
            Player p1 = ConstructNAddPlayer();
            Player p2 = ConstructNAddPlayer();

            bool p1priority = false;
            bool correctPhase = false;

            EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => p1priority = evt.Player == p1));
            EventManager.Register(new Trigger<PhaseChangedEvent>(evt => correctPhase = evt.NewPhase == Phases.Main && evt.OldPhase == Phases.Draw));

            EventManager.Raise(new GameStartEvent());

            EventManager.Raise(new PlayerPassedPriorityEvent(p1));
            EventManager.Raise(new PlayerPassedPriorityEvent(p2));

            Assert.IsTrue(p1priority);
            Assert.IsTrue(correctPhase);
        }




        private Player ConstructNAddPlayer()
        {
            Player p = new Player();
            p.Name = "some player";
            EventManager.Raise(new PlayerJoinedEvent(p));
            return p;
        }
    }
}
