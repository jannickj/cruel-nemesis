using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ConsoleXmasImplementation.Model;
using ConsoleXmasImplementation.Model.Entities;
using XmasEngineController.AI;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace ConsoleXmasImplementation.Controller
{
	public class HumanInterfaceManager : AgentManager
	{
		private KeyboardSettings settings;
		private AutoResetEvent playerFlagEvent = new AutoResetEvent(false);

		public HumanInterfaceManager(KeyboardSettings settings)
		{
			this.settings = settings;
		}

		public override void Initialize()
		{
			this.EventManager.Register(new Trigger<EntityAddedEvent>(game_EntityAdded));
		}

		protected override Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor()
		{
			return AgentControllerConstructor;
		}

		private KeyValuePair<string, AgentController> AgentControllerConstructor()
		{
			Agent player = this.TakeControlOf("player");
			var kv = new KeyValuePair<string, AgentController>(player.Name,new HumanInterfaceController(player,settings));

			return kv;
		}

		public override void Start()
		{
			playerFlagEvent.WaitOne();
			var con = AgentControllerConstructor();
			con.Value.Start();
		}

		private void game_EntityAdded(EntityAddedEvent evt)
		{
			if (evt.AddedXmasEntity is Player)
			{
				this.playerFlagEvent.Set();
			}
		}

		public override string ThreadName()
		{
			return "Human Controller";
		}

	}
}
