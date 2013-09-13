using System;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.Management;
using ConsoleXmasImplementation.Model.Events;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using System.Collections.Generic;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class DropZone : ConsoleEntity
	{
        private List<Package> packages = new List<Package>();
   
		public DropZone ()
		{
			RuleBasedMovementBlockingModule blockingModule = (RuleBasedMovementBlockingModule)this.Module<MovementBlockingModule>();
			blockingModule.AddNewRuleLayer<DropZone>();
			blockingModule.AddWillNotBlockRule<DropZone>(_ => true);
		}

		protected override void OnLoad()
		{
            this.EventManager.Register(new Trigger<PackageReleasedEvent>(world_packageReleased));
		}


        private void world_packageReleased(PackageReleasedEvent evt)
        {
            Point here = ((TilePosition)this.World.GetEntityPosition(this)).Point;
            Point package = ((TilePosition)this.World.GetEntityPosition(evt.ReleasedPackage)).Point;

            if (here == package)
            {
                this.packages.Add(evt.ReleasedPackage);
                this.World.RemoveEntity(evt.ReleasedPackage);

            }
        }

        
	}
}

