using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineView;

namespace XmasEngineExtensions.LoggerExtension
{
	public class LoggerView : XmasView
	{
		private ThreadSafeEventQueue evtq;
		private Logger log;

		protected Logger Log
		{
			get { return log; }
		
		}

		public LoggerView(XmasModel model, Logger log) : base(new ThreadSafeEventManager())
		{
			this.log = log;
			this.evtq = model.EventManager.ConstructEventQueue();
			this.ThreadSafeEventManager.AddEventQueue(evtq);
			this.evtq.Register(new Trigger<ActionFailedEvent>(engine_ActionFailed));
		}

		public override void Start()
		{
			while (true)
				ThreadSafeEventManager.ExecuteNextWhenReady();
		}

		private void engine_ActionFailed(ActionFailedEvent evt)
		{
			log.LogStringWithTimeStamp (evt.Exception.Message, DebugLevel.Error);
		}
	}
}
