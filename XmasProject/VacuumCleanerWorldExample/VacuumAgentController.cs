using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSLibrary.Data.GenericEvents;
using VacuumCleanerWorldExample.Actions;
using VacuumCleanerWorldExample.Percepts;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;

namespace VacuumCleanerWorldExample
{
	public class VacuumAgentController : AgentController
	{
		private EntityXmasAction nextAction = null;

		public VacuumAgentController(Agent agent) : base(agent)
		{
			this.PerceptsRecieved += agent_perceptRecieved;
		}

		//Override this method is the first and only thing executed in its controller thread
		public override void Start()
		{
			while (true)
			{
				//Force the vacuum cleaner to generate all its percepts
				this.performAction(new GetAllPerceptsAction());

				//Perform its next action based on its decission
				if (this.nextAction != null)
					this.performAction(this.nextAction);

			}

		}

		//this method will be called when an agent recieves a percept
		private void agent_perceptRecieved(object sender, UnaryValueEvent<PerceptCollection> evt)
		{
			//go through all percepts
			foreach (Percept percept in evt.Value.Percepts)
			{

				if (percept is VacuumVision)
				{
					//Check if the vacuum cleaner is in a tile with dirt then call suck
					//otherwise move the vacuum cleaner
					VacuumVision vision = percept as VacuumVision;

					if (vision.ContainsDirt)
						this.nextAction = new SuckAction();
					else
						this.nextAction = new MoveVacuumCleanerAction();
				}
			}
		}
	}
}
