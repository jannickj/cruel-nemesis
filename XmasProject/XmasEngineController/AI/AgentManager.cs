using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using JSLibrary;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Interfaces;

namespace XmasEngineController.AI
{

    /// <summary>
    /// A manager for handling multiple agent controllers, this is responsible for creating new instances of AgentControllers and give them their own thread
    /// </summary>
	public abstract class AgentManager : XmasController, IStartable
	{

        /// <summary>
        /// The main method of the AgentManager, by default this will repeatably without stop attempt to generate new controllers
        /// </summary>
		public override void Start()
		{
			while (true)
			{
				Func<KeyValuePair<string, AgentController>> agentcontroller = AquireAgentControllerContructor();
				Thread thread = null;
				thread = Factory.CreateThread(() => agent_Thread(agentcontroller));
				thread.Start();
			}
		}

        /// <summary>
        /// Override this method for an implementation of the AgentManager to control how its AgentControllers are constructed.
        /// </summary>
        /// <returns>An explaination of how to construct the controller</returns>
		protected abstract Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor();


		private void agent_Thread(Func<KeyValuePair<string, AgentController>> constructor)
		{
			KeyValuePair<string, AgentController> agent;
			try
			{
				if (this.AgentControllerConstructionTimeOut == 0)
				{
					agent = constructor();
				}
				else
					Parallel.TryExecute(constructor, this.AgentControllerConstructionTimeOut, out agent);

				Thread.CurrentThread.Name = agent.Key+" Thread";
				
				agent.Value.Start();
			}
			catch (TimeoutException)
			{
				throw new TimeoutException("Agent Controller construction timed out");
			}
		}

        /// <summary>
        /// The agent controller attempts to locate an agent in the world
        /// </summary>
        /// <param name="name">name of the agent that is being located</param>
        /// <returns>The agent with the given name</returns>
		public Agent TakeControlOf(string name)
		{
			lock (this)
			{
				Agent agent;
				if (this.World.TryGetAgent(name, out agent))
				{
					return agent;
				}
				else
					throw new Exception("Agent controller was unable to assume control of " + name);
			}
		}

        /// <summary>
        /// Maximum time for AgentController to construct. If timeout is 0 the time is infinitely. Default is 0.
        /// </summary>
		public virtual int AgentControllerConstructionTimeOut
		{
			get
			{
				return 0;
			}
		}
		

		

		
	}
}