using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacuumCleanerWorldExample
{
	public class VacuumMap1 : VacuumWorldBuilder
	{
		public VacuumMap1()
		{
			//This explains to the engine how to generate one instantiation of the map
			//if the user wishes to make multiple different map then it would be need to make new classes
			//that extends the VacuumWorldBuilder
			this.AddEntity(() => new VacuumCleanerAgent("vacuum_cleaner"),new VacuumSpawnInformation(0));
			this.AddEntity(() => new DirtEntity(), new VacuumSpawnInformation(1));
		}
	}
}
