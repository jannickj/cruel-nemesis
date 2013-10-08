using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Unit;
using JSLibrary.Data;

namespace Assets.GameLogic.Events
{
	public class BeginMoveEvent : XmasEvent
	{
        private UnitEntity unit;
        private Point from, to;
        private int duration;

        public int Duration
        {
            get { return duration; }
        }

        public UnitEntity Unit
        {
            get { return unit; }
        }

        public Point To
        {
            get { return to; }
        }

        public Point From
        {
            get { return from; }
        }

        public BeginMoveEvent(UnitEntity u, Point from, Point to, int dur)
        {
            this.unit = u;
            this.from = from;
            this.to = to;
            this.duration = dur;
        }
	}
}
