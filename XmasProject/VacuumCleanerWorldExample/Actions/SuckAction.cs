using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Events;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace VacuumCleanerWorldExample.Actions
{
	public class SuckAction : EntityXmasAction<VacuumCleanerAgent>
	{
		//Override this for the action to be executable
		protected override void Execute()
		{
			//goes through all entities at the vacuum cleaner's position and removes all dirt
			foreach (XmasEntity ent in this.World.GetEntities(this.Source.Position))
			{
				if (ent is DirtEntity)
					this.World.RemoveEntity(ent);
			}

			//Raises the event that the vacuum cleaner has sucked
			this.Source.Raise(new VacuumSuckedEvent());

			//Tells the engine that the action is done
			this.Complete();

		}
	}
}
