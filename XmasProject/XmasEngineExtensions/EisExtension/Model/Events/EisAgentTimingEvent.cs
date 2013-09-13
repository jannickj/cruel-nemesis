using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Model.Events
{
	public class EisAgentTimingEvent : XmasEvent
	{
		private Agent agent;
		private string description;
		private TimeSpan timespan;

		public Agent Agent
		{
			get { return agent; }
		}

		public string Description
		{
			get { return description; }
		}

		public TimeSpan TimeSpan
		{
			get { return timespan; }
		}

		public EisAgentTimingEvent(Agent agent, string description, TimeSpan timespan)
		{
			this.agent = agent;
			this.description = description;
			this.timespan = timespan;
		}
	}
}
