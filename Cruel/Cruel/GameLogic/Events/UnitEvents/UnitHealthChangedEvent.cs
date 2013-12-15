using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events.UnitEvents
{
    public class UnitHealthChangedEvent : XmasEvent
    {
        private int oldhp;
        private int newhp;

        public int OldHp
        {
            get { return oldhp; }
        }
        

        public int NewHp
        {
            get { return newhp; }
        }

        public UnitHealthChangedEvent(int oldhp, int newhp)
        {
            // TODO: Complete member initialization
            this.oldhp = oldhp;
            this.newhp = newhp;
        }
    }
}
