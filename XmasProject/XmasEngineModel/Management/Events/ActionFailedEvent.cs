using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Management.Events
{

    public class ActionFailedEvent<TAction> : ActionFailedEvent where TAction : XmasAction
    {
        public new TAction FailedAction
        {
            get
            {
                return (TAction)base.FailedAction;
            }
        }

        public ActionFailedEvent(TAction failedAction, Exception e) : base(failedAction, e)
        {

        }
    }

	public class ActionFailedEvent : XmasEvent
	{
		private Exception e;
        private XmasAction failedAction;

        

		internal ActionFailedEvent(XmasAction failedAction, Exception e)
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
