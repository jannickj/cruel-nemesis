using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.TurnLogic;
using Assets.UnityLogic.Gui;
using Assets.GameLogic;
using UnityEngine;

namespace Assets.UnityLogic.Commands
{
	public class ToggleStopPriorityCommand : Command
	{
        private Phases phase;
        private PhaseSkipController skipController;
        private Player stopOnPlayer;

        public ToggleStopPriorityCommand(PhaseSkipController skipController, Player stopOnPlayer, Phases phase)
        {
            this.phase = phase;
            this.stopOnPlayer = stopOnPlayer;
            this.skipController = skipController;
        }

        public override void Update()
        {
            this.skipController.TogglePhaseSkip(stopOnPlayer, phase);
            this.Finished = true;
        }
    }
}
