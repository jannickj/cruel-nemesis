using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Events;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel;
using XmasEngineModel.Management;

namespace VacuumCleanerWorldExample
{
	public class VacuumWorldView : LoggerView
	{
		private ThreadSafeEventQueue evtq;

		public VacuumWorldView(XmasModel model,Logger log) : base(model,log)
		{
			//Construct an ThreadSafe Event queue which triggers can be registered to while keeping the code thread safe
			this.evtq = model.EventManager.ConstructEventQueue();
			this.ThreadSafeEventManager.AddEventQueue(evtq);

			//Register the triggers that track the vacuum cleaners actions
			this.evtq.Register(new Trigger<VacuumMovedEvent>(vacuum_Moved));
			this.evtq.Register(new Trigger<VacuumSuckedEvent>(vacuum_Sucked));
		}

		//Log that the Vacuum cleaner sucked
		private void vacuum_Sucked(VacuumSuckedEvent obj)
		{
			this.Log.LogStringWithTimeStamp("Vacuum sucked", DebugLevel.All);
		
		}

		//Log that the vacuum cleaner moved
		private void vacuum_Moved(VacuumMovedEvent obj)
		{
			this.Log.LogStringWithTimeStamp("Vacuum moved from: " + obj.From + ", to: " + obj.To,DebugLevel.All);
		}

		
	}
}
