using ConsoleXmasImplementation.Model.Entities;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.Model.Events
{
	public class PackageReleasedEvent : XmasEvent
	{
		public Package ReleasedPackage { get; private set; }

        public PackageReleasedEvent(Package package)
		{
            ReleasedPackage = package;
		}
	}
}

