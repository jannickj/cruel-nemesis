using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.TurnLogic;
using Assets.UnityLogic.Gui;
using Assets.GameLogic;

namespace Assets.UnityLogic.Commands
{
	public class ToggleStopPriorityCommand : Command
	{
        private Phases phase;

        public ToggleStopPriorityCommand(PhaseSkipController skipController, Player stopOnPlayer, Phases phase)
        {
            this.phase = phase;
        }

        public override void Update()
        {
            //this.GuiController.SetSkipPhase(phase);
            this.Finished = true;
        }
    }
}
