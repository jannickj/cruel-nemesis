using System;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel;
using System.Collections.Generic;
using XmasEngineModel.Percepts;
using ConsoleXmasImplementation.Model.Entities;
using XmasEngineModel.World;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model.Modules
{
	public class PackageGrabbingModule : UniversalModule<XmasEntity>
	{
        private Package package = null;

		public PackageGrabbingModule()
		{
		
		}


		public override IEnumerable<Percept> Percepts {
			get {
                if (package != null)
					return new Percept[] { new EmptyNamedPercept ("holdingPackage") };

				return new Percept[0];
			}
		}

        public void Grab(Package package)
        {
            if (this.package != null || package == null)
                return;

            this.package = package;
            this.World.RemoveEntity(package);

        }

        public Package Drop()
        {
            if (this.package == null)
                return null;
            Package dropped = this.package;
            this.World.AddEntity(package,this.Host.Position.GenerateSpawn());
            package = null;

            return dropped;
        }
    }
}

