using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmasEngineModel;

namespace VacuumCleanerWorldExample
{
	public class VacuumWorldBuilder : XmasWorldBuilder
	{
		//This explains to the engine how to construct the world we just made
		protected override XmasWorld ConstructWorld()
		{
			return new VacuumWorld();
		}
	}
}