using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Exceptions;
using Cruel.GameLogic.Actions;
using XmasEngineModel.Management.Actions;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Events.UnitEvents;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.TurnLogic
{
	public class TurnManager : XmasUniversal
	{
        private List<Player> players = new List<Player>();
        private Player playersTurn = null;
        private Player playerWithPriority = null;
        private Phases currentPhase = Phases.End;
        private Queue<Player> priorityQueue = new Queue<Player>();
        private Dictionary<UnitEntity, PlayerDeclareMoveAttackEvent> moveAttackDeclaration = new Dictionary<UnitEntity, PlayerDeclareMoveAttackEvent>();
        private int declareStep = 0;


        public Phases CurrentPhase { get { return this.currentPhase; } }
        public Player PlayersTurn { get { return this.playersTurn; } }
        public Player PlayerWithPriority { get { return this.playerWithPriority; } }

        protected override void  OnAddedToEngine()
        {
            this.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoined));
            this.EventManager.Register(new Trigger<PlayerPassedPriorityEvent>(OnPlayerPassedPriority));
            this.EventManager.Register(new Trigger<GameStartEvent>(OnGameStart));
            this.EventManager.Register(new Trigger<PlayerPerformedActionEvent>(OnPlayerPerformedAction));
            this.EventManager.Register(new Trigger<PlayerDeclareMoveAttackEvent>(OnPlayerDeclareMoveAttack));
            this.EventManager.Register(new Trigger<ResetPrioritiesEvent>(OnResetPriorities));
        }

        private void OnResetPriorities(ResetPrioritiesEvent evt)
        {
            this.ResetPriority();
        }

        private void OnPlayerJoined(PlayerJoinedEvent evt)
        {
            if(!players.Contains(evt.Player))
                players.Add(evt.Player);
        }

        private void OnPlayerDeclareMoveAttack(PlayerDeclareMoveAttackEvent evt)
        {
            if (currentPhase == Phases.Declare && evt.Player == playersTurn)
            {
                PlayerDeclareMoveAttackEvent oldevt;
                if (moveAttackDeclaration.TryGetValue(evt.Entity, out oldevt) && oldevt.AttackUnit != null)
                {
                    oldevt.AttackUnit.Raise(new UnitDeclaredAsAttacked(evt.AttackUnit, evt.Entity, false));
                }
                                
                moveAttackDeclaration[evt.Entity] = evt;

                if (evt.AttackUnit != null)
                {
                    var attackee = evt.Entity;
                    var attacked = evt.AttackUnit;
                    evt.AttackUnit.Raise(new UnitDeclaredAsAttacked(attacked, attackee, true));
                }

            }
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
            if(this.playerWithPriority != null)
                this.playerWithPriority.HasPriority = false;
            this.playerWithPriority = p;
            if(p!=null)
                p.HasPriority = true;
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
            {
                this.declareStep = 0;
                this.moveAttackDeclaration.Clear();
                SetTurn(turnplayer);
            }
            if (prioReset)
                SetPhase(newphase);
            if (prioReset)
                ResetPriority();
            else
                SetPriority(prioplayer);

            if (currentPhase == Phases.Declare)
            {
                switch (this.declareStep)
                {
                    case 0:
                        declareStep = 1;
                        this.EventManager.Raise(new PlayerAllowedToDeclareMoveAttackEvent(this.playersTurn, true));
                        break;
                    case 1:
                        declareStep = 2;
                        this.EventManager.Raise(new PlayerAllowedToDeclareMoveAttackEvent(this.playersTurn, false));
                        break;
                }
            }
            if (currentPhase == Phases.Move)
                PerformMoves();
            if (currentPhase == Phases.Attack)
                PerformAttacks();

        }

        private void PerformMoves()
        {
            MultiAction ma = new MultiAction();
            foreach (PlayerDeclareMoveAttackEvent evt in this.moveAttackDeclaration.Values)
            {
                MovePathAction mpa = evt.MoveAction;
                ma.AddAction(mpa);
            }
            ma.Resolved += (sender, maEvt) => SkipToPhase(Phases.React,this.playersTurn);
            this.ActionManager.Queue(ma);
        }

        private void PerformAttacks()
        {
            MultiAction ma = new MultiAction();
            foreach (PlayerDeclareMoveAttackEvent evt in this.moveAttackDeclaration.Values)
            {
                
                AttackUnitAction aua = evt.AttackAction;
                if(evt.AttackUnit != null)
                    ma.AddAction(evt.Entity,aua);
            }
            ma.Resolved += (sender, maEvt) => SkipToPhase(Phases.EndCombat,this.playersTurn);
            this.ActionManager.Queue(ma);
        }

        private void SkipToPhase(Phases phase, Player playersTurn)
        {
            this.SetPhase(phase);
            this.SetPriority(playersTurn);
               
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
            var evt = new PhaseChangingEvent();
            this.EventManager.Raise(evt);

            if (evt.PreventedPhaseChange)
            {
                phaseReset = false;
                return currentPhase;
            }

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
            this.priorityQueue.Clear();

            foreach (Player p in players)
            {
                if (p != playersTurn)
                    this.priorityQueue.Enqueue(p);
            }
            if (this.currentPhase == Phases.Move || this.currentPhase == Phases.Attack)
            {
                this.SetPriority(null);
            }
            else
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
