using System;
using System.Threading;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace XmasEngineController.AI
{
    /// <summary>
    /// An abstract AgentController meant for implementations of the AgentController to extend
    /// </summary>
	public abstract class AgentController
	{
		private AutoResetEvent actionComplete = new AutoResetEvent(false);
        private Agent agent;
		private PerceptCollection newpercepts;

        /// <summary>
        /// Constructs the AgentController
        /// </summary>
        /// <param name="agent">Name of the agent the controller is meant to control</param>
		public AgentController(Agent agent)
		{
			this.agent = agent;
			agent.Register(new Trigger<RetreivePerceptsEvent>(agent_RetrievePercepts));
		}

        /// <summary>
        /// Gets the agent controlled by the controller
        /// </summary>
        public Agent Agent
        {
            get { return agent; }
        }

        /// <summary>
        /// Event that occurs when the performAction method is called and new percepts are retrieved from the agent.
        /// This event is threadsafe since it is called on the same thread that called performAction.
        /// </summary>
		protected event UnaryValueHandler<PerceptCollection> PerceptsRecieved;

        /// <summary>
        /// Performs an action on a certain agent, the method is frozen until the action has completed.
        /// </summary>
        /// <param name="action">The action that is being performed</param>
		public void performAction(EntityXmasAction action)
		{
			action.Resolved += action_Completed;
			agent.QueueAction(action);

			actionComplete.WaitOne();

			PerceptCollection activepercepts = null;
			lock (this)
			{
				activepercepts = newpercepts;
				newpercepts = null;
			}

			if (PerceptsRecieved != null && activepercepts != null && action.ActionFailed == false)
			{
				PerceptsRecieved(this, new UnaryValueEvent<PerceptCollection>(activepercepts));
			}
		}

        /// <summary>
        /// Override this to provide the controller with a main method on which decissions can be taken
        /// </summary>
		public abstract void Start();

		#region EVENTS

		private void action_Completed(object sender, EventArgs e)
		{
			actionComplete.Set();
			((XmasAction) sender).Resolved -= action_Completed;
		}

		private void agent_RetrievePercepts(RetreivePerceptsEvent e)
		{
			lock (this)
			{
				newpercepts = e.Percepts;
			}
		}

		#endregion

	}
}