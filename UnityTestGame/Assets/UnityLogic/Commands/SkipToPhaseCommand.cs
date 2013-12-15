using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.TurnLogic;
using Assets.UnityLogic.Gui;

namespace Assets.UnityLogic.Commands
{
	public class SkipToPhaseCommand : Command
	{
        private Gui.PhaseSkipToController SkipController;
        private Cruel.GameLogic.TurnLogic.Phases selectedPhase;

        public SkipToPhaseCommand(PhaseSkipToController SkipController, Phases selectedPhase)
        {
            // TODO: Complete member initialization
            this.SkipController = SkipController;
            this.selectedPhase = selectedPhase;
        }
        public override void Update()
        {
            SkipController.SkipToPhase(selectedPhase);
            this.Finished = true;
        }
    }
}
