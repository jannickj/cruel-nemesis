using System;
using System.Timers;


namespace XmasEngineModel.Management.Actions
{

    /// <summary>
    /// A timer that queues its action to the engine when it expires
    /// </summary>
	public class TimedAction : EnvironmentAction
	{
		private Action action;
		private bool single;
		private DateTime stopped;
		private Timer timer = new Timer();

        /// <summary>
        /// Instantiates a Timed Action
        /// </summary>
        /// <param name="action">The action that is queued onto the engine when the timer expires</param>
		public TimedAction(Action action)
		{
			this.action = action;
			timer.AutoReset = false;

			timer.Elapsed += timer_Elapsed;
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!single)
                lock(timer)
				    timer.Start();

			SimpleAction sa = new SimpleAction(_ => action());
			sa.Failed += simpleAction_Failed;
            sa.PreExecution += sa_PreExecution;
            this.MakeActionChild(sa);
			this.ActionManager.Queue(sa);
		}

        void sa_PreExecution(object sender, EventArgs e)
        {
            XmasAction act = (XmasAction)sender;
            this.MakeActionChild(act);
        }

		void simpleAction_Failed(object sender, EventArgs e)
		{
            this.Fail();

			((SimpleAction) sender).Failed -= simpleAction_Failed;
		}



        /// <summary>
        /// Starts the timer to run until the timer has expired
        /// </summary>
        /// <param name="milisec">The time in milli seconds that the timer runs for</param>
		public void SetSingle(double milisec)
		{
			single = true;
            lock(timer)
                timer.Interval = milisec;
		}

        /// <summary>
        /// Starts the timer to run periodically, will queue an action to the engine for each time
        /// </summary>
        /// <param name="milisec">The time in milli seconds one periodic loop takes</param>
		public void SetPeriodic(double milisec)
		{
			single = false;
            lock(timer)
                timer.Interval = milisec;
		}

        /// <summary>
        /// Stops the timer completely(does not gaurrantee the action is not queued just at the last possible moment)
        /// </summary>
		public void Stop()
		{

            lock (timer)
            {
                stopped = DateTime.Now;
                timer.Stop();
            }
		}

        protected override void Execute()
        {
            timer.Start();
        }

        protected override bool IsAutoCompleting
        {
            get
            {
                return false;
            }
        }
    }
}