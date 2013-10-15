using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Management.Events
{
	public class ActionFailedEvent : XmasEvent
	{
		private Exception e;
        private XmasAction failedAction;

        

		public ActionFailedEvent(XmasAction failedAction, Exception e)
		{
			this.e = e;
            this.failedAction = failedAction;
		}

        public XmasAction FailedAction
        {
            get { return failedAction; }
        }

		public Exception Exception
		{
			get { return e; }
		}
	}
}
