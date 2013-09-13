using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Management.Events
{
	public class ActionFailedEvent : XmasEvent
	{
		private Exception e;

		public ActionFailedEvent(Exception e)
		{
			this.e = e;
		}

		public Exception ActionException
		{
			get { return e; }
		}
	}
}
