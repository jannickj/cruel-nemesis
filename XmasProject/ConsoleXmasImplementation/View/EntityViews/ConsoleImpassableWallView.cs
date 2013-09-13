using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleImpassableWallView : ConsoleEntityView
	{
		public ConsoleImpassableWallView(XmasEntity model, XmasPosition position, ThreadSafeEventManager evtman)
			: base(model, position, evtman)
		{
		}

		public override char Symbol
		{
			get { return 'I'; }
		}
	}
}