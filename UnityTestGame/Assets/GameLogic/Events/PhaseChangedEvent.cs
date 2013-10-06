using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.TurnLogic;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
	public class PhaseChangedEvent : XmasEvent
	{
        private TurnLogic.Phases oldphase;
        private TurnLogic.Phases newPhase;



        public PhaseChangedEvent(Phases oldphase, Phases newPhase)
        {
            this.oldphase = oldphase;
            this.newPhase = newPhase;
        }
	}
}
