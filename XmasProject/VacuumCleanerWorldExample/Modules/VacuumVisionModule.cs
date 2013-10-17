using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Percepts;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;

namespace VacuumCleanerWorldExample.Modules
{
	public class VacuumVisionModule : UniversalModule<XmasEntity>
	{
		//Override this method for the module to provide percepts to the VacuumCleaner
		public override IEnumerable<Percept> Percepts
		{
			get
			{
				//This module checkes if there are any dirt located on the Vacuum cleaners position
				//if dirt is located it returns true if not it returns false as part of the vision percept
				if (Host.World.GetEntities(this.Host.Position).Any(Ent => Ent is DirtEntity))
					return new Percept[] { new VacuumVision(true,(VacuumPosition)Host.Position) };
				else
					return new Percept[] { new VacuumVision(false, (VacuumPosition)Host.Position) };

			}
		}

	}
}
