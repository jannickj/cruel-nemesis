using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JSLibrary
{
    /// <summary>
    /// Class that provides minor parallel functionallity
    /// </summary>
	public class Parallel
	{
        /// <summary>
        /// Executes a function and halts it if it execeeds its timeout
        /// </summary>
        /// <typeparam name="TResult">The result type that is being calculated by the function</typeparam>
        /// <param name="func">The function that does the calculation</param>
        /// <param name="timeout">The time out in milli seconds</param>
        /// <returns>The result</returns>
        /// <exception cref="TimeoutException"></exception>
		public static TResult Execute<TResult>(Func<TResult> func, int timeout)
		{
			TResult result;
			TryExecute(func, timeout, out result);
			return result;
		}

        /// <summary>
        /// Attempts to execute a function within a timelimit and returns whether or not this was possible
        /// </summary>
        /// <typeparam name="TResult">The result type that is being calculated by the function</typeparam>
        /// <param name="func">The function that does the calculation</param>
        /// <param name="timeout">The time out in milli seconds</param>
        /// <param name="result">The result being calculated</param>
        /// <returns>Whether or not the execution was within the timelimit</returns>
        /// <exception cref="TimeoutException"></exception>
		public static bool TryExecute<TResult>(Func<TResult> func, int timeout, out TResult result)
		{
			TResult t = default(TResult);
			Thread thread = new Thread(() => t = func());
			thread.Start();
			bool completed = thread.Join(timeout);
			if (!completed)
			{
				thread.Abort();
				throw new TimeoutException();
			}
			result = t;
			return completed;
		}

        /// <summary>
        /// Executes an action, on set timer interval a condition must be satisfied or the execution of thread is halted
        /// </summary>
        /// <param name="action">The action being executed</param>
        /// <param name="intervalMiliSec">The time between the condition checks</param>
        /// <param name="ThreadAbortCondition">The condition for aborting the thread, if satisfied the thread is stopped</param>
        public static void ExecuteWithPollingCheck(Action action, double intervalMiliSec, Func<bool> ThreadAbortCondition)
        {
            Thread t = Thread.CurrentThread;
            System.Timers.Timer timer = new System.Timers.Timer(intervalMiliSec);
            timer.Elapsed += delegate
            {  
                if(ThreadAbortCondition())
                {
                    timer.Stop();
                  
                    t.Abort();

                }
            };

            action();
            timer.Stop();

        }
	}
}
