using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using Assets.GameLogic.Exceptions;

namespace Assets.GameLogic.TurnLogic
{
	public class TurnManager : XmasActor
	{
        private List<Player> players = new List<Player>();
        private Player playersTurn = null;
        private Player playerWithPriority = null;
        private Phases currentPhase = Phases.End;
        private Queue<Player> priorityQueue = new Queue<Player>();

        public void Initialize()
        {

            this.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoined));
            this.EventManager.Register(new Trigger<PlayerPassedPriorityEvent>(OnPlayerPassedPriority));
            this.EventManager.Register(new Trigger<GameStartEvent>(OnGameStart));
            this.EventManager.Register(new Trigger<PlayerPerformedActionEvent>(OnPlayerPerformedAction));

        }

        private void OnPlayerJoined(PlayerJoinedEvent evt)
        {
            if(!players.Contains(evt.Player))
                players.Add(evt.Player);
        }

        private void OnPlayerPassedPriority(PlayerPassedPriorityEvent evt)
        {
            if (evt.Player == playerWithPriority)
            {
                ChangePriorityPhaseTurn();
            }
            else
                throw new IllegalPlayerPriorityException(IllegalPriorityActions.Passed_Priority, evt.Player);
        }

        private void OnPlayerPerformedAction(PlayerPerformedActionEvent evt)
        {
            if (evt.Player == playerWithPriority)
                ResetPriority();
            else
                throw new IllegalPlayerPriorityException(IllegalPriorityActions.Performed_Action, evt.Player);
        }

        private void OnGameStart(GameStartEvent evt)
        {
            ChangePriorityPhaseTurn();
        }

        private void ChangePriorityPhaseTurn()
        {
            if (ChangePriority())
                if (ChangePhase())
                    ChangeTurn();
        }

        private void ChangeTurn()
        {
            if (playersTurn == null)
            {
                playersTurn = players[0];
            }
            else
                playersTurn = findNextPlayerTurn();

            this.EventManager.Raise(new PlayersTurnChangedEvent(playersTurn));
        }

        private bool ChangePhase()
        {
            bool phasereset;
            Phases oldphase = currentPhase;
            if (currentPhase == Phases.End)
            {
                currentPhase = Phases.Draw;
                phasereset = true;
            }
            else
            {
                currentPhase++;
                phasereset = false;
            }

            this.EventManager.Raise(new PhaseChangedEvent(oldphase, currentPhase));
            return phasereset;

        }

        private void ResetPriority()
        {
            playerWithPriority = playersTurn;

            foreach (Player p in players)
            {
                if (p != playersTurn)
                    this.priorityQueue.Enqueue(p);
            }
            this.EventManager.Raise(new PlayerGainedPriorityEvent(playerWithPriority));
        }

        private bool ChangePriority()
        {
            if (priorityQueue.Count == 0)
            {
                ResetPriority();
                return true;
            }
            else
            {
                playerWithPriority = priorityQueue.Dequeue();
                this.EventManager.Raise(new PlayerGainedPriorityEvent(playerWithPriority));
                return false;
            }

            
            
        }

        private Player findNextPlayerTurn()
        {
            int i = players.FindIndex(p => p == playersTurn);
            i = i >= players.Count ? 0 : i + 1;
                        
            return players[i];
        }
        
	}
}
