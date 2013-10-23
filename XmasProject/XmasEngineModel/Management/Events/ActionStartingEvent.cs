using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Management.Events
{
    /// <summary>
    /// Event that is fired when an action has been started through the engine
    /// </summary>
    /// <typeparam name="TAction">Type of action that has been started</typeparam>
    public class ActionStartingEvent<TAction> : XmasEvent where TAction : XmasAction
    {

        /// <summary>
        /// Gets the action that was started
        /// </summary>
        public TAction Action { get; private set; }

        internal ActionStartingEvent(TAction action)
        {
            this.Action = action;
        }

    }
}
