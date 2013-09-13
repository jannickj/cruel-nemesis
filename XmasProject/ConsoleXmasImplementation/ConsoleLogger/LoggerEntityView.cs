using System;
using XmasEngineView;
using XmasEngineModel.World;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using System.IO;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineExtensions;
using ConsoleXmasImplementation.Model.Events;
using XmasEngineExtensions.EisExtension.Model.Events;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class LoggerEntityView : EntityView
	{
		private Point pos;
		private Logger logstream;

		public LoggerEntityView (XmasEntity model
                               , XmasPosition position
		                       , ThreadSafeEventManager evtman 
		                       , Logger logstream
		) : base(model, position, evtman)
		{
			this.logstream = logstream;
			eventqueue.Register (new Trigger<UnitMovePostEvent> (entity_UnitMovePostEvent));
			eventqueue.Register (new Trigger<UnitMovePreEvent> (entity_UnitMovePreEvent));
			eventqueue.Register (new Trigger<PackageGrabbedEvent> (entity_PackageGrabbedEvent));
		}

		private void entity_UnitMovePostEvent (UnitMovePostEvent evt)
		{
			if (pos.Equals (evt.NewPos))
				return;

			string info = String.Format ("{{{0}}} finished moving from {1} to {2}", model, pos, evt.NewPos);
			logstream.LogStringWithTimeStamp (info, DebugLevel.Info);
			pos = evt.NewPos;
		}

		private void entity_UnitMovePreEvent (UnitMovePreEvent evt)
		{
			string info = String.Format("{{{0}}} started moving from {1} to {2}", model, pos, evt.NewPos);
			logstream.LogStringWithTimeStamp (info, DebugLevel.All);
		}

		private void entity_PackageGrabbedEvent(PackageGrabbedEvent evt)
		{
			string info = String.Format ("{{{0}}} grabbed the package {{{1}}}", model, evt.GrabbedPackage);
			logstream.LogStringWithTimeStamp (info, DebugLevel.Info);
		}

		#region implemented abstract members of EntityView

		public override XmasPosition Position {
			get { return new TilePosition (pos); }
			protected set { this.pos = ((TilePosition)value).Point; }
		}

		#endregion
	}
}

