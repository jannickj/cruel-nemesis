using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Management.Events
{
    public class TriggerFailedEvent : XmasEvent
    {
        private Trigger failedtrigger;
        private Exception exception;

        public Exception Exception
        {
            get { return exception; }
        }

        public Trigger FailedTrigger
        {
            get { return failedtrigger; }
        }


        public TriggerFailedEvent(Trigger failedtrigger, Exception exception)
        {
            this.failedtrigger = failedtrigger;
            this.exception = exception;
        }


    }
}
