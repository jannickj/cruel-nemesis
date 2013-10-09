using JSLibrary.Data.GenericEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Actions
{
    public class MultiAction : EnvironmentAction
    {

        public event UnaryValueHandler<XmasAction> SingleActionCompleted;
        public event EventHandler SequenceCompleted;
        private Queue<Action> actionQueue = new Queue<Action>();
        private bool stopMultiAction = false;

        /// <summary>
        /// Instantiates a MultiAction meant for running a sequence of actions
        /// </summary>
        public MultiAction()
        {

        }

        /// <summary>
        /// Add an Entity action to the sequence
        /// </summary>
        /// <param name="ent">Entity the action is executed on</param>
        /// <param name="action">Action to be executed in the sequence</param>
        public void AddAction(XmasEntity ent, EntityXmasAction action)
        {
            actionQueue.Enqueue(() =>
                {
                    this.SetupAction(action);
                    ent.QueueAction(action);
                    
                });
        }

        /// <summary>
        /// Stops the multi action from continuing its sequence of actions
        /// </summary>
        public void StopMultiAction()
        {
            this.stopMultiAction = true;
        }

        void action_PostExecution(object sender, EventArgs e)
        {
            XmasAction action = (XmasAction)sender;
            action.PostExecution -= action_PostExecution;
            if (SingleActionCompleted != null)
                SingleActionCompleted(this, new UnaryValueEvent<XmasAction>(action));
            if (!stopMultiAction)
                executeNext();
        }

        /// <summary>
        /// Adds an environment action to the sequence
        /// </summary>
        /// <param name="action">Action to be added to the sequence</param>
        public void AddAction(EnvironmentAction action)
        {
            actionQueue.Enqueue(() =>
            {
                this.SetupAction(action);
                this.ActionManager.Queue(action);                
            });
        }

        private void SetupAction(XmasAction action)
        {
            this.MakeActionChild(action);
            action.PostExecution +=action_PostExecution;
        }

        private void executeNext()
        {
            if (this.actionQueue.Count == 0)
            {
                if (SequenceCompleted != null)
                    SequenceCompleted(this, new EventArgs());
            }
            else
            {

                Action func = this.actionQueue.Dequeue();
                func();
            }
        }


        protected override void Execute()
        {
            executeNext();
        }
    }
}
