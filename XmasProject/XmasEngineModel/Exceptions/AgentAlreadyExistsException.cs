using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Exceptions
{
	public class AgentAlreadyExistsException : Exception
	{
		private Agent agent;
		private Agent otheragent;
		private string name;

		public Agent Agent
		{
			get { return agent; }
		}

		

		public string Name
		{
			get { return name; }
		}

		public Agent OtherAgent
		{
			get { return otheragent; }
		
		}

		public AgentAlreadyExistsException(Agent agent, Agent otheragent)
			: base("Attempted to add agent of type " + agent.GetType().Name + ", but another agent of type "+otheragent.GetType().Name+". Already has the name: "+agent.Name)
		{
			this.agent = agent;
			this.otheragent = otheragent;
			this.name = agent.Name;

		}
	}
}
