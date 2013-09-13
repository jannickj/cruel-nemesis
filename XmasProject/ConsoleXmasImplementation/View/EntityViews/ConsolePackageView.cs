using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsolePackageView : ConsoleEntityView
	{
        public ConsolePackageView(XmasEntity model, XmasPosition position, ThreadSafeEventManager evtman)
			:base(model, position, evtman)
		{
		}

		#region implemented abstract members of ConsoleEntityView

		public override char Symbol {
			get { return 'X'; }
		}

		#endregion
	}
}

