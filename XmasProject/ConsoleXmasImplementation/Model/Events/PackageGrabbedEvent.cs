using ConsoleXmasImplementation.Model.Entities;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.Model.Events
{
	public class PackageGrabbedEvent : XmasEvent
	{
		public Package GrabbedPackage { get; private set; }

		public PackageGrabbedEvent (Package grabbedPackage)
		{
			GrabbedPackage = grabbedPackage;
		}
	}
}

