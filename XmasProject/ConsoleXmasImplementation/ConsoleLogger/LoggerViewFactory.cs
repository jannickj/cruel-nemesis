using System;
using XmasEngineView;
using XmasEngineModel.Management;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel.World;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class LoggerViewFactory : ViewFactory
	{
		private ThreadSafeEventManager evtman;
		private Logger logstream;

		public LoggerViewFactory (ThreadSafeEventManager evtman, Logger logstream)
		{
			this.evtman = evtman;
			this.logstream = logstream;
		}

		#region implemented abstract members of ViewFactory

        public override EntityView ConstructEntityView(XmasEntity model, XmasPosition position)
		{
			return new LoggerEntityView(model, position, evtman, logstream);
		}

		#endregion
	}
}

