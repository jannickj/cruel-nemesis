using ConsoleXmasImplementation.Model.Events;
using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;


namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleGrabberView : ConsoleEntityView
	{
        private bool haspackage = false;

        public ConsoleGrabberView(XmasEntity model, XmasPosition position, ThreadSafeEventManager evtman)
			: base(model, position, evtman)
		{

            this.EventQueue.Register(new Trigger<PackageGrabbedEvent>(_ => haspackage = true));
            this.eventqueue.Register(new Trigger<PackageReleasedEvent>(_ => haspackage = false));

		}

		public override char Symbol {
			get 
            {
                if (haspackage)
                {
                    return 'G';
                }
                else
                    return 'A';
            }
		}
	}
}

