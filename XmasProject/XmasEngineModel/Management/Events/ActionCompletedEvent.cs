using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Management.Events
{

    /// <summary>
    /// Event that is fired when an action has completed in the engine
    /// </summary>
    /// <typeparam name="TAction">Type of action that has completed</typeparam>
    public class ActionCompletedEvent<TAction> : XmasEvent where TAction : XmasAction
    {
        /// <summary>
        /// Gets the action that completed
        /// </summary>
        public TAction Action { get; private set; }

        internal ActionCompletedEvent(TAction action)
        {
            this.Action = action;
        }

    }
}
