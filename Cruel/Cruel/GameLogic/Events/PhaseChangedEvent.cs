using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.TurnLogic;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events
{
	public class PhaseChangedEvent : XmasEvent
	{
        private Phases oldphase;
        private Phases newPhase;

        public PhaseChangedEvent(Phases oldphase, Phases newPhase)
        {
            this.oldphase = oldphase;
            this.newPhase = newPhase;
        }

        public Phases OldPhase
        {
            get { return oldphase; }
        }

        public Phases NewPhase
        {
            get { return newPhase; }
        }
	}
}
