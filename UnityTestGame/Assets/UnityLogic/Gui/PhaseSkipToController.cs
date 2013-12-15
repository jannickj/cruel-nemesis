using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using Cruel.GameLogic.TurnLogic;
using JSLibrary.Data;
using UnityEngine;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.PlayerCommands;

namespace Assets.UnityLogic.Gui
{
	public class PhaseSkipToController
	{
        private GuiInformation guiinfo;
        private Player owner;
        private Player lastGainPrio;
        private Player currentPlayersTurn;
        private Phases curPhase;
        private Phases skipToPhase;
        private bool shouldSkipToPhase = false;
        private bool prioritiesHasBeenReset = false;

        public PhaseSkipToController(GuiInformation guiinfo, Player owner)
        {
            // TODO: Complete member initialization
            this.guiinfo = guiinfo;
            this.owner = owner;
            owner.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => { currentPlayersTurn = evt.PlayersTurn; shouldSkipToPhase = false; }));
            owner.EventManager.Register(new Trigger<PhaseChangedEvent>(evt => { lastGainPrio = null; prioritiesHasBeenReset = false; curPhase = evt.NewPhase; }));
            owner.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(this.OnPlayerGainPriority));
            owner.EventManager.Register(new Trigger<ResetPrioritiesEvent>(evt => prioritiesHasBeenReset = true));
            
        }

        public void SkipToPhase(Phases phase)
        {
            if (((int)phase) <= ((int)curPhase))
                return;
            prioritiesHasBeenReset = false;
            skipToPhase = phase;
            shouldSkipToPhase = true;
            CheckIfShouldSkip();
        }

        

        private void OnPlayerGainPriority(PlayerGainedPriorityEvent evt)
        {
            this.lastGainPrio = evt.Player;
            if (evt.Player != owner)
                return;
            
            CheckIfShouldSkip();

        }


        private void CheckIfShouldSkip()
        {
            if (currentPlayersTurn != this.owner)
                owner.ActionManager.Queue(new PlayerPassPriorityCommand(owner));
            if (!shouldSkipToPhase || prioritiesHasBeenReset)
                return;

            if(skipToPhase == curPhase)
                shouldSkipToPhase = false; 

            if (skipToPhase != curPhase || curPhase == Phases.End)
            {
                owner.ActionManager.Queue(new PlayerPassPriorityCommand(owner));

            }
        }
        
	}
}
