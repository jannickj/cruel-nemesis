using ConsoleXmasImplementation.Model.Entities;
using ConsoleXmasImplementation.View.EntityViews;
using System;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;
using XmasEngineView;

namespace ConsoleXmasImplementation.View
{
	public class ConsoleViewFactory : ViewFactory
	{
		#region implemented abstract members of ViewFactory

		private ThreadSafeEventManager evtman;

		public ConsoleViewFactory(ThreadSafeEventManager evtman)
		{
			this.evtman = evtman;
			AddTypeLink<Ghost, ConsoleGhostView>();
			AddTypeLink<Wall, ConsoleWallView>();
			AddTypeLink<Player, ConsolePlayerView>();
			AddTypeLink<ImpassableWall, ConsoleImpassableWallView>();
			AddTypeLink<GrabberAgent, ConsoleGrabberView>();
			AddTypeLink<Package, ConsolePackageView>();
			AddTypeLink<DropZone, ConsoleDropZoneView>();
		}

		public override EntityView ConstructEntityView(XmasEntity model, XmasPosition position)
		{
			ConsoleEntityView retval = (ConsoleEntityView) Activator.CreateInstance(typeDict[model.GetType()], model, position, evtman);
           
			return retval;
		}

		#endregion
	}
}