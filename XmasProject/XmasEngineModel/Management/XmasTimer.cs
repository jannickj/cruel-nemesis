using System;
using System.Timers;
using XmasEngineModel.Management.Actions;

namespace XmasEngineModel.Management
{

    /// <summary>
    /// A timer that queues its action to the engine when it expires
    /// </summary>
	public class XmasTimer
	{
		private Action action;
		private ActionManager actman;
		private bool single;
		private DateTime stopped;
		private Timer timer = new Timer();
		private XmasAction owner;

        /// <summary>
        /// Instantiates a XmasTimer
        /// </summary>
        /// <param name="actman">The ActionManager of the engine</param>
        /// <param name="owner">The XmasAction that owns the timer</param>
        /// <param name="action">The action that is queued onto the engine when the timer expires</param>
		public XmasTimer(ActionManager actman, XmasAction owner, Action action)
		{
			this.owner = owner;
			this.actman = actman;
			this.action = action;
			timer.AutoReset = false;

			timer.Elapsed += timer_Elapsed;
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!single)
				timer.Start();

			SimpleAction sa = new SimpleAction(_ => action());
			sa.Failed += simpleAction_Failed;

			actman.Queue(sa);
		}

		void simpleAction_Failed(object sender, EventArgs e)
		{
			this.owner.Fail();

			((SimpleAction) sender).Failed -= simpleAction_Failed;
		}

		private void start(double m)
		{
			timer.Interval = m;
			timer.Start();
		}

        /// <summary>
        /// Starts the timer to run until the timer has expired
        /// </summary>
        /// <param name="milisec">The time in milli seconds that the timer runs for</param>
		public void StartSingle(double milisec)
		{
			single = true;
			start(milisec);
		}

        /// <summary>
        /// Starts the timer to run periodically, will queue an action to the engine for each time
        /// </summary>
        /// <param name="milisec">The time in milli seconds one periodic loop takes</param>
		public void StartPeriodic(double milisec)
		{
			single = false;
			start(milisec);
		}

        /// <summary>
        /// Stops the timer
        /// </summary>
		public void Stop()
		{
			stopped = DateTime.Now;
			timer.Stop();
		}
	}
}