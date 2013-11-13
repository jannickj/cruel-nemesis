using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Unit;
using JSLibrary.Data;

namespace Cruel.GameLogic.Events
{
	public class PreMoveEvent : XmasEvent
	{
        private UnitEntity unit;
        private Point from, to;
        private bool moveStopped = false;

        public bool MoveStopped
        {
            get { return moveStopped; }
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

        public PreMoveEvent(UnitEntity u, Point from, Point to)
        {
            this.unit = u;
            this.from = from;
            this.to = to;
        }

        public void StopMove() 
        {
            moveStopped = true;
        }
	}
}
