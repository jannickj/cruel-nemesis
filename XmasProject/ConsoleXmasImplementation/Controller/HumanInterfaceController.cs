using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using JSLibrary.Data;
using XmasEngineController.AI;
using XmasEngineExtensions.TileExtension.Actions;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;

namespace ConsoleXmasImplementation.Controller
{
	public class HumanInterfaceController : AgentController
	{
		private KeyboardSettings settings;
		private EntityXmasAction currentAction;
		private Dictionary<Char, EntityXmasAction> actionMap = new Dictionary<char, EntityXmasAction>(); 

		public HumanInterfaceController(Agent agent, KeyboardSettings settings) : base(agent)
		{
			this.settings = settings;
			
			this.actionMap.Add(settings.MoveUp(),new MoveUnitAction(new Vector(0,1)));
			this.actionMap.Add(settings.MoveDown(),new MoveUnitAction(new Vector(0,-1)));
			this.actionMap.Add(settings.MoveRight(),new MoveUnitAction(new Vector(1,0)));
			this.actionMap.Add(settings.MoveLeft(),new MoveUnitAction(new Vector(-1,0)));

			this.currentAction = this.actionMap[this.settings.MoveUp()];
		}

		public override void Start()
		{
			while (true)
			{
				var key = new ConsoleKeyInfo();
				bool keyread = false;


				while (Console.KeyAvailable) 
				{
					var newkey = Console.ReadKey(true);
					if (actionMap.ContainsKey(newkey.KeyChar))
					{
						key = newkey;
						keyread = true;
					}

				}

                if (keyread == true)
                    currentAction = this.actionMap[key.KeyChar];
                else
                    continue;

				this.performAction(currentAction);
				
				this.performAction(new GetAllPerceptsAction());
				
			}
		}
	}
}
