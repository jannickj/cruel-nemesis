using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Events;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace VacuumCleanerWorldExample.Actions
{
	public class MoveVacuumCleanerAction : EntityXmasAction<VacuumCleanerAgent>
	{
		//Override this for the action to be executable
		protected override void Execute()
		{
			
			//Create a timer so the move is delayed by a certain speed
			//(Otherwise the agent will be able to move instantanious only limited by CPU power)
			XmasTimer timer = this.Factory.CreateTimer(this, () =>
				{
					//the old position of the vacuum cleaner
					var pos = (VacuumPosition)this.Source.Position;

					//the new position of the vacuum cleaner 
					XmasPosition newpos = new VacuumPosition(-1);

					//Moves the Vacuum to the other tile ei. if on tile 0 move it to tile 1
					if (pos.PosID == 0)
						newpos = new VacuumPosition(1);
					else if (pos.PosID == 1)
						newpos = new VacuumPosition(0);

					this.World.SetEntityPosition(this.Source, newpos);

					//Raises the event that the vacuum cleaner has moved
					this.Source.Raise(new VacuumMovedEvent(pos, newpos));

					//Tells the engine the action is complete
					this.Complete();
				});

			//Start the timer, when the timer is done the action added above will be queued safely to the engine
			timer.StartSingle(1000);

			
		}
	}
}
