using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events.UnitEvents
{
    public class UnitMaxHealthChangedEvent : XmasEvent
    {
        private int oldMax;
        private int newMax;

        public int OldMax
        {
            get { return oldMax; }
        }
        

        public int NewMax
        {
            get { return newMax; }
        }

        public UnitMaxHealthChangedEvent(int oldMax, int newMax)
        {
            // TODO: Complete member initialization
            this.oldMax = oldMax;
            this.newMax = newMax;
        }
        

    }
}
