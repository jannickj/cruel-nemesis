using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmasEngineModel.World;

namespace VacuumCleanerWorldExample
{
	public class VacuumSpawnInformation : EntitySpawnInformation
	{
		//Since the vacuum cleaner doesn't require more information upon entering the world this information is empty
		public VacuumSpawnInformation(int spawn) : base(new VacuumPosition(spawn))
		{
			
		}

	
	}
}
