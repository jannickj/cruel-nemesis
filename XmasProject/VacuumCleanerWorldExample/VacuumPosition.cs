using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.World;

namespace VacuumCleanerWorldExample
{
	public class VacuumPosition : XmasPosition
	{
		public int PosID { get; set; }

		public VacuumPosition(int posId)
		{
			this.PosID = posId;
		}

		public override EntitySpawnInformation GenerateSpawn()
		{
			return new VacuumSpawnInformation(PosID);
		}

		public override string ToString()
		{
			return "Tile("+PosID+")";
		}
	}
}
