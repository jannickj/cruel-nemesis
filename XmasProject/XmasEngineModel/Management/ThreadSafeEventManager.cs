using JSLibrary.ParallelLib;
using System;
using System.Threading;

namespace XmasEngineModel.Management
{

    /// <summary>
    /// A Manager for managing ThreadSafeEventQueues, the manager can execute events stored on eventqueues added to it.
    /// </summary>
	public class ThreadSafeEventManager
	{
		private ConcurrentQueue<Action> awaitingEvents = new ConcurrentQueue<Action>();
		private AutoResetEvent waitForItemEvent = new AutoResetEvent (false);

        /// <summary>
        /// Adds an EventQueue to the manager, all events picked up by that queue can be fired through the manager
        /// </summary>
        /// <param name="queue"></param>
		public void AddEventQueue(ThreadSafeEventQueue queue)
		{
			queue.EventRecieved += queue_EventRecieved;
		}

		private void queue_EventRecieved(object sender, EventArgs e)
		{
			awaitingEvents.Enqueue (() => ((ThreadSafeEventQueue)sender).ExecuteNext ());
			waitForItemEvent.Set ();
		}

        /// <summary>
        /// Executes the next event stored on one of the eventqueues
        /// </summary>
        /// <returns>Whether or not the execution was successful</returns>
		public bool ExecuteNext()
		{
			Action a;
			bool retval;
			if (awaitingEvents.TryDequeue (out a)) {
				a ();
				retval = true;
			} else {
				retval = false;
			}

			if (!awaitingEvents.IsEmpty)
				waitForItemEvent.Set ();

			return retval;
		}

        /// <summary>
        /// Freezes the thread until a new Event has been queued to one of the eventqueues, that it will proceed to execute
        /// </summary>
		public void ExecuteNextWhenReady()
		{
			waitForItemEvent.WaitOne ();
			ExecuteNext ();
		}

        /// <summary>
        /// Freezes the thread with a timeout until a new Event has been queued to one of the eventqueues, that it will proceed to execute
        /// </summary>
        /// <param name="ts">The timeout that says how long the thread will maximum freeze</param>
		public void ExecuteNextWhenReady(TimeSpan ts)
		{
			waitForItemEvent.WaitOne (ts);
			ExecuteNext ();
		}

        /// <summary>
        /// Freezes the thread with a timeout until a new Event has been queued to one of the eventqueues, that it will proceed to execute
        /// </summary>
        /// <param name="ts">The timeout that says how long the thread will maximum freeze</param>
        /// <param name="slept">The time in ticks, there are 10,000 ticks in a millisecond.</param>
        public void ExecuteNextWhenReady(TimeSpan ts, out long slept)
        {
            DateTime start = DateTime.Now;
            waitForItemEvent.WaitOne(ts);
            slept = DateTime.Now.Ticks - start.Ticks;
            ExecuteNext();
        }
	}
}