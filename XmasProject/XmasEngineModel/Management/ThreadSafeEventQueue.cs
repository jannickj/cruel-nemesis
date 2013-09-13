using System;
using JSLibrary.Data.GenericEvents;
using JSLibrary.ParallelLib;


namespace XmasEngineModel.Management
{
    /// <summary>
    /// A ThreadSafe EventQueue meant to store events raises on entities and the EventManager 
    /// </summary>
	public class ThreadSafeEventQueue : IDisposable
	{
		private TriggerManager foreignTriggermanager;
		private ConcurrentQueue<XmasEvent> queue = new ConcurrentQueue<XmasEvent>();
		private TriggerManager triggerManager = new TriggerManager();

       
		internal ThreadSafeEventQueue(TriggerManager unsafeTriggers)
		{
			foreignTriggermanager = unsafeTriggers;
			foreignTriggermanager.EventRaised += foreignTriggermanager_EventRaised;
		}

		internal event EventHandler EventRecieved;


		internal bool ExecuteNext()
		{
			XmasEvent res;
			if (queue.TryDequeue(out res))
			{
				triggerManager.Raise(res);
				return true;
			}
			return false;
		}

        /// <summary>
        /// Registers a trigger to the Threadsafe Eventqueue
        /// </summary>
        /// <param name="trigger">The trigger registered</param>
		public void Register(Trigger trigger)
		{
			triggerManager.Register(trigger);
		}

		private void foreignTriggermanager_EventRaised(object sender, UnaryValueEvent<XmasEvent> evt)
		{
			EventHandler buffer = EventRecieved;
			this.queue.Enqueue(evt.Value);

			if (buffer != null)
				buffer(this, new EventArgs());
		}


		public void Dispose()
		{
			foreignTriggermanager.EventRaised -= foreignTriggermanager_EventRaised;
		}
	}
}