using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace VacuumCleanerWorldExample.Events
{
	public class VacuumMovedEvent : XmasEvent
	{
        //The position the vacuum moved from
		public XmasPosition From{get; private set;}

        //The position the vacuum moved to
		public XmasPosition To { get; private set; }

		public VacuumMovedEvent(XmasPosition from, XmasPosition to)
		{
			this.From = from;
			this.To = to;
		}
	}
}
