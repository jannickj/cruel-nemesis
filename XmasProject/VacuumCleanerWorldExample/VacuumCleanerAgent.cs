using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Modules;
using XmasEngineModel.EntityLib;

namespace VacuumCleanerWorldExample
{
	public class VacuumCleanerAgent : Agent
	{
		public VacuumCleanerAgent(string name)
			: base(name)
		{
            //Register the Vacuum vision module to the agent, to allow it to recieve percepts
			this.RegisterModule(new VacuumVisionModule());
			
		}
	}
}
