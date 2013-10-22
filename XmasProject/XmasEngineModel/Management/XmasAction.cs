using System;
using System.Collections.Generic;
using XmasEngineModel.Attributes;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management
{


	public abstract class XmasAction : XmasActor, ICloneable
	{
        private XmasAction parentAction = null;
		public event EventHandler Completed;
		public event EventHandler Failed;
		public event EventHandler Resolved;
        public event EventHandler PreExecution;
        public event EventHandler PostExecution;
		private bool actionfailed = false;
        private Exception actionfailedException = null;
        private HashSet<XmasAction> subactions = new HashSet<XmasAction>();

        /// <summary>
        /// gets whether or not the action failed
        /// </summary>
		public bool ActionFailed
		{
			get { return actionfailed; }
		}

        /// <summary>
        /// gets the exception the action threw when it failed, if no exception caused it to fail this will return null
        /// </summary>
        public Exception ActionFailedException
        {
            get { return this.actionfailedException; }
        }

        public XmasAction Parent
        {
            get { return parentAction; }
        }

		internal void Fire()
		{
            if (PreExecution != null)
                PreExecution(this, new EventArgs());
			Execute();
            if(IsAutoCompleting)
                TestComplete();
		}

       

        /// <summary>
        /// Override this method to provide the ability for the action to execute
        /// </summary>
		protected abstract void Execute();

        /// <summary>
        /// Calling this indicates to the engine that the action has completed, this method should only be called once and only when the action is truly completed
        /// </summary>
        [Obsolete("No longer necessary",false)]
		protected void Complete()
		{
			

		}

        protected virtual bool IsAutoCompleting
        {
            get { return true; }
        }


        /// <summary>
        /// Run action as child action of this action
        /// </summary>
        /// <param name="action">action to be executed</param>
        public void RunAction(EnvironmentAction action)
        {
            this.MakeActionChild(action);
            ActionManager.ExecuteAction(action);
        }

        /// <summary>
        /// Run action as child action of this action
        /// </summary>
        /// <param name="entity">entity the action is put on</param>
        /// <param name="action">action to be executed</param>
        public void RunAction(XmasEntity entity, EntityXmasAction action)
        {
            this.MakeActionChild(action);
            action.Source = entity;
            ActionManager.ExecuteAction(action);
        }

        protected void MakeActionChild(XmasAction child)
        {
            child.SetParent(this);
            lock (this)
            {
                this.subactions.Add(child);
            }
            child.Resolved += child_Resolved;
        }

        void child_Resolved(object sender, EventArgs e)
        {
            lock (this)
            {
                this.subactions.Remove((XmasAction)sender);
            }
            TestComplete();
        }


        private void TestComplete()
        {
            int count;
            lock(this)
                count = subactions.Count;
            
            if (count == 0)
            {
                if (PostExecution != null)
                    PostExecution(this, new EventArgs());
                EventHandler buffer = Completed;
                if (buffer != null)
                    buffer(this, new EventArgs());

                Resolve();
            }
        }

        /// <summary>
        /// Calling this indicates to the engine that the action has failed.
        /// </summary>
		public void Fail(Exception e = null)
		{
			this.actionfailed = true;
            this.actionfailedException = e;
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

        internal void SetParent(XmasAction parent)
        {
            this.parentAction = parent;
        }

        [VirtualNotImplemented("The action is not thread safe if clonable is not implemented")]
        public virtual object Clone()
        {
            return this;
        }
    }
}