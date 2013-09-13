using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data.GenericEvents;

namespace XmasEngineModel.Management
{

    /// <summary>
    /// A trigger meant to contain multiple events, conditions and actions at a time
    /// </summary>
	public class MultiTrigger : Trigger
	{
		private Dictionary<object, Action<XmasEvent>> actionDictionary = new Dictionary<object, Action<XmasEvent>>();
		private HashSet<Action<XmasEvent>> actions = new HashSet<Action<XmasEvent>>();
		private Dictionary<object, Predicate<XmasEvent>> condDictionary = new Dictionary<object, Predicate<XmasEvent>>();
		private HashSet<Predicate<XmasEvent>> conditions = new HashSet<Predicate<XmasEvent>>();
		private HashSet<Type> eventtypes = new HashSet<Type>();

        /// <summary>
        /// Gets a collection of event type that the trigger is triggered by
        /// </summary>
		public override ICollection<Type> Events
		{
			get { return eventtypes.ToArray(); }
		}

        /// <summary>
        /// Gets all the predicates that must be satisfied for the trigger to fire
        /// </summary>
		public ICollection<Predicate<XmasEvent>> Conditions
		{
			get { return conditions.ToArray(); }
		}

        /// <summary>
        /// Gets all actions meant to be fired by the trigger
        /// </summary>
		public ICollection<Action<XmasEvent>> Actions
		{
			get { return actions.ToArray(); }
		}

		internal event UnaryValueHandler<Type> RegisteredEvent;
		internal event UnaryValueHandler<Type> DeregisteredEvent;

        /// <summary>
        /// Registers a type of event that the trigger is triggered by
        /// </summary>
        /// <typeparam name="T">The event type that triggers the trigger</typeparam>
		public void RegisterEvent<T>() where T : XmasEvent
		{
			Type type = typeof (T);
			eventtypes.Add(type);
			if (RegisteredEvent != null)
				RegisteredEvent(this, new UnaryValueEvent<Type>(type));
		}

        /// <summary>
        /// Deregisters an event from the trigger, so that the trigger is no longer triggered by that event
        /// </summary>
        /// <typeparam name="T">The event type that is deregistered</typeparam>
		public void DeregisterEvent<T>() where T : XmasEvent
		{
			Type type = typeof (T);
			eventtypes.Remove(type);
			if (DeregisteredEvent != null)
				DeregisteredEvent(this, new UnaryValueEvent<Type>(type));
		}

		internal void RemoveAction<T>(Action<T> action) where T : XmasEvent
		{
			removeObject(action, actionDictionary, actions);
		}

		internal void AddAction<T>(Action<T> action) where T : XmasEvent
		{
			addObject(e => action((T) e), action, actionDictionary, actions);
		}

		internal void AddCondition<T>(Predicate<T> condition) where T : XmasEvent
		{
			addObject(e => condition((T) e), condition, condDictionary, conditions);
		}

		internal void RemoveCondition<T>(Predicate<T> condition) where T : XmasEvent
		{
			removeObject(condition, condDictionary, conditions);
		}

		private void addObject<T>(T wrapper, object o, IDictionary<object, T> dic, ICollection<T> list)
		{
			dic.Add(o, wrapper);
			list.Add(wrapper);
		}

		private void removeObject<T>(object o, IDictionary<object, T> dic, HashSet<T> list)
		{
			T wrapper;
			if (!dic.TryGetValue(o, out wrapper))
				return;

			dic.Remove(o);

			list.Remove(wrapper);
		}

        internal protected override bool CheckCondition(XmasEvent evt)
		{
			return Conditions.All(C => C(evt));
		}

		internal protected override void Execute(XmasEvent evt)
		{
			foreach (Action<XmasEvent> A in Actions)
			{
				A(evt);
			}
		}
	}
}