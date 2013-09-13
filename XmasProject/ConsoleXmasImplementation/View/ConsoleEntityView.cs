using JSLibrary.Data;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineModel.World;
using XmasEngineView;

namespace ConsoleXmasImplementation.View
{
	public abstract class ConsoleEntityView : EntityView
	{
		private Point pos = new Point();

        public ConsoleEntityView(XmasEntity model, XmasPosition position, ThreadSafeEventManager evtman)
            : base(model, position, evtman)
		{

			eventqueue.Register(new Trigger<UnitMovePostEvent>(entityView_UnitMovePostEvent));
		}

		public abstract char Symbol { get; }

		public override XmasPosition Position
		{
			get { return new TilePosition(pos); }
			protected set { this.pos = ((TilePosition)value).Point; }
		}

		private void entityView_UnitMovePostEvent(UnitMovePostEvent evt)
		{
			this.pos = evt.NewPos;
		}
	}
}