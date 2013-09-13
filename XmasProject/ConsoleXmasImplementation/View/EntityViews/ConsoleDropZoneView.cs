using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleDropZoneView : ConsoleEntityView
	{
		public ConsoleDropZoneView(XmasEntity model, XmasPosition position, ThreadSafeEventManager evtman)
			: base(model, position, evtman)
		{
		}
		
		public override char Symbol
		{
			get { return 'D'; }
		}
	}
}