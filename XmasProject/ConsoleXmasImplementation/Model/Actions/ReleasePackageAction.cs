using ConsoleXmasImplementation.Model.Entities;
using ConsoleXmasImplementation.Model.Events;
using ConsoleXmasImplementation.Model.Modules;
using System.Collections.Generic;
using System.Linq;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;

namespace ConsoleXmasImplementation.Model.Actions
{
	public class ReleasePackageAction : EntityXmasAction<Agent>
	{
		public ReleasePackageAction ()
		{
		}

		protected override void Execute ()
		{


            Package dropped = Source.Module<PackageGrabbingModule>().Drop();

            if (dropped == null)
                this.Fail();
			

            Source.Raise(new PackageReleasedEvent(dropped));
			Complete ();
			
		}
	}
}

