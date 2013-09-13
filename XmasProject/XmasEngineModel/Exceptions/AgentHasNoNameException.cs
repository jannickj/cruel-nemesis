using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Exceptions
{
	public class AgentHasNoNameException : Exception
	{
		private Agent agent;

		public Agent Agent
		{
			get { return agent; }
			set { agent = value; }
		}

		public AgentHasNoNameException(Agent agent)
			: base("Attempted to add agent of type " + agent.GetType().Name + ", without a name")
		{
			this.agent = agent;
		}
	}
}
