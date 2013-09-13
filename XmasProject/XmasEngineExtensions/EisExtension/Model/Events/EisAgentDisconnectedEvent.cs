using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Model.Events
{
    public class EisAgentDisconnectedEvent : XmasEvent
    {
        private Agent agent;
	    private Exception exception;


        public Agent Agent
        {
            get { return agent; }
        }

	    public Exception Exception
	    {
		    get { return exception; }
	    }

	    public EisAgentDisconnectedEvent(Agent agent, Exception exception)
        {
            this.agent = agent;
		    this.exception = exception;
        }
    }
}
