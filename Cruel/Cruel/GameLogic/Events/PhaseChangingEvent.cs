using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events
{
    public class PhaseChangingEvent : XmasEvent
    {
        public bool PreventedPhaseChange { get; private set; }

        public PhaseChangingEvent()
        {
            this.PreventedPhaseChange = false;
        }

        public void SetPreventPhase()
        {
            this.PreventedPhaseChange = true;
        }

    }
}
