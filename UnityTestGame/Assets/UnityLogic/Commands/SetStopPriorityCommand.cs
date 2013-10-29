using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.TurnLogic;

namespace Assets.UnityLogic.Commands
{
	public class ToggleStopPriorityCommand : Command
	{
        private Phases phase;

        public ToggleStopPriorityCommand(Phases phase)
        {
            this.phase = phase;
        }

        public override void Update()
        {
            this.GuiController.GuiInfo.TogglePhase(phase);
            this.Finished = true;
        }
    }
}
