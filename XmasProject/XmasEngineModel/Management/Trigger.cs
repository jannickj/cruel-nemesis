using System;
using System.Collections.Generic;
using XmasEngineModel.Management.Interfaces;

namespace XmasEngineModel.Management
{

    /// <summary>
    /// Trigger is the most basic form of a trigger
    /// </summary>
	public abstract class Trigger : ITrigger
	{
        /// <summary>
        /// Gets all events on the trigger
        /// </summary>
		public abstract ICollection<Type> Events { get; }

        /// <summary>
        /// Checks the condition on the trigger
        /// </summary>
        /// <param name="evt">The event that triggered the trigger</param>
        /// <returns>Whether or not the condition is satisfied</returns>
        internal protected abstract bool CheckCondition(XmasEvent evt);

        /// <summary>
        /// Executes the action of the trigger
        /// </summary>
        /// <param name="evt">The event that triggered the trigger</param>
        internal protected abstract void Execute(XmasEvent evt);
	}

    /// <summary>
    /// A trigger triggered only by the TEvent
    /// </summary>
    /// <typeparam name="TEvent">The event that triggers the trigger</typeparam>
	public class Trigger<TEvent> : Trigger where TEvent : XmasEvent
	{
		private Action<TEvent> action;
		private Predicate<TEvent> condition;
		private Type evt = typeof (TEvent);

        /// <summary>
        /// Instantiates a trigger tied to a specific event with no condition and a single action
        /// </summary>
        /// <param name="action">The action the trigger fires</param>
		public Trigger(Action<TEvent> action)
		{
			this.action = action;
			condition = (_ => true);
		}

        /// <summary>
        /// Instantiates a trigger tied to a specific event with a single condition and a single action
        /// </summary>
        /// <param name="condition">The condition the trigger is tied to</param>
        /// <param name="action">The action fire by the trigger</param>
		public Trigger(Predicate<TEvent> condition, Action<TEvent> action)
		{
			this.condition = condition;
			this.action = action;
		}

        /// <summary>
        /// Returns the event the trigger is triggered by
        /// </summary>
		public override ICollection<Type> Events
		{
			get { return new[] {evt}; }
		}

        /// <summary>
        /// Executes the condition of the trigger
        /// </summary>
        /// <param name="evt">The event of the trigger</param>
        /// <returns>whether or not the condition is satisfied</returns>
		internal protected override bool CheckCondition(XmasEvent evt)
		{
			return condition((TEvent) evt);
		}

        /// <summary>
        /// Execute the action on the trigger
        /// </summary>
        /// <param name="evt">The event that triggered the trigger</param>
        internal protected override void Execute(XmasEvent evt)
		{
			action((TEvent) evt);
		}

        public override string ToString()
        {
            return "trigger<"+typeof(TEvent).Name+">";
        }
	}
}