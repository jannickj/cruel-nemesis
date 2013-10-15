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
            SetTurn(players[0]);
            SetPhase(Phases.Draw);
            ResetPriority();
        }

        private void SetTurn(Player p)
        {
            this.playersTurn = p;
            this.EventManager.Raise(new PlayersTurnChangedEvent(p));
        }

        private void SetPhase(Phases phase)
        {
            Phases oldp = this.currentPhase;
            this.currentPhase = phase;
            this.EventManager.Raise(new PhaseChangedEvent(oldp, currentPhase));
        }

        private void SetPriority(Player p)
        {
            this.playerWithPriority = p;
            this.EventManager.Raise(new PlayerGainedPriorityEvent(p));
        }

        private void ChangePriorityPhaseTurn()
        {
            bool prioReset = false;
            Player prioplayer = null;
            bool phaseReset = false;
            Phases newphase = Phases.Draw;
            Player turnplayer = null;

            prioplayer = NextPriority(out prioReset);
            if(prioReset)
                newphase = GetNextPhase(out phaseReset);
            if (phaseReset)
                turnplayer = GetNextTurn();


            if (phaseReset)
                SetTurn(turnplayer);
            if (prioReset)
                SetPhase(newphase);
            if (prioReset)
                ResetPriority();
            else
                SetPriority(prioplayer);

        }

        private Player GetNextTurn()
        {
            
            int i = players.FindIndex(p => p == playersTurn) + 1;
            i = i >= players.Count ? 0 : i;
            return players[i];
            
        }

        

        private Phases GetNextPhase( out bool phaseReset)
        {
            Phases next;
            if (currentPhase == Phases.End)
            {
                phaseReset = true;
                next = Phases.Draw;
            }
            else
            {
                phaseReset = false;
                next = currentPhase + 1;
               
            }            
            return next;

        }

        private void ResetPriority()
        {
            
            foreach (Player p in players)
            {
                if (p != playersTurn)
                    this.priorityQueue.Enqueue(p);
            }

            this.SetPriority(playersTurn);
            
        }

        private Player NextPriority(out bool reset)
        {
            if (priorityQueue.Count == 0)
            {
                reset = true;
                return null;
            }
            else
            {
                reset = false;
                return priorityQueue.Dequeue();
            }

            
            
        }

        
        
	}
}
