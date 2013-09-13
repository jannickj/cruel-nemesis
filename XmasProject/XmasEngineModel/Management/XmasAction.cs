using System;

namespace XmasEngineModel.Management
{


	public abstract class XmasAction : XmasActor
	{
		public event EventHandler Completed;
		public event EventHandler Failed;
		public event EventHandler Resolved;
		private bool actionfailed = false;

        /// <summary>
        /// gets whether or not the action failed
        /// </summary>
		public bool ActionFailed
		{
			get { return actionfailed; }
		}

		internal void Fire()
		{
			Execute();
		}

        /// <summary>
        /// Override this method to provide the ability for the action to execute
        /// </summary>
		protected abstract void Execute();

        /// <summary>
        /// Calling this indicates to the engine that the action has completed, this method should only be called once and only when the action is truly completed
        /// </summary>
		protected void Complete()
		{
			EventHandler buffer = Completed;
			if (buffer != null)
				buffer(this, new EventArgs());

			Resolve();

		}

        /// <summary>
        /// Calling this indicates to the engine that the action has failed.
        /// </summary>
		public void Fail()
		{
			this.actionfailed = true;
			EventHandler buffer = Failed;
			if (buffer != null)
				buffer(this, new EventArgs());
			Resolve();

		}

		private void Resolve()
		{
			EventHandler buffer = Resolved;
			if (buffer != null)
				buffer(this, new EventArgs());
		}
	}
}