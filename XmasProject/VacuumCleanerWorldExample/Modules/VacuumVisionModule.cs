using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Percepts;
using XmasEngineModel;
using XmasEngineModel.EntityLib.Module;

namespace VacuumCleanerWorldExample.Modules
{
	public class VacuumVisionModule : EntityModule
	{
		//Override this method for the module to provide percepts to the VacuumCleaner
		public override IEnumerable<Percept> Percepts
		{
			get
			{
				//This module checkes if there are any dirt located on the Vacuum cleaners position
				//if dirt is located it returns true if not it returns false as part of the vision percept
				if (EntityHost.World.GetEntities(this.EntityHost.Position).Any(Ent => Ent is DirtEntity))
					return new Percept[] { new VacuumVision(true,(VacuumPosition)EntityHost.Position) };
				else
					return new Percept[] { new VacuumVision(false, (VacuumPosition)EntityHost.Position) };

			}
		}

	}
}
