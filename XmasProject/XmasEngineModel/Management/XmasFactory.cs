using System;
using System.Threading;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Percepts;
using XmasEngineModel.Management.Actions;

namespace XmasEngineModel.Management
{
    /// <summary>
    /// Object factory for the engine's actors to use, should be extended to allow actors to generate new kinds of objects.
    /// </summary>
	public class XmasFactory
	{
		private ActionManager actman;

        /// <summary>
        /// Instantiates a XmasFactory
        /// </summary>
        /// <param name="actman">The action manager used by the engine</param>
		public XmasFactory(ActionManager actman)
		{
			this.actman = actman;
		}


        /// <summary>
        /// Creates a specially designed timer that queues its action to the engine when the timer has expired, thus this timer is threadsafe
        /// </summary>
        /// <param name="owner">The action that is owner of the timer</param>
        /// <param name="action">The action that the timer executes</param>
        /// <returns>The timer</returns>
		public TimedAction CreateTimer(Action action)
		{
			TimedAction gt = new TimedAction(action);
			return gt;
		}

        /// <summary>
        /// Creates a thread with an action that is executed when the thread is started
        /// </summary>
        /// <param name="action">The act</param>
        /// <returns></returns>
		public Thread CreateThread(Action action)
		{
			return new Thread(new ThreadStart(action));
		}
	}
}
