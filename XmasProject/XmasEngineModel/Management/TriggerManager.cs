using System;
using System.Collections.Generic;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Management.Events;

namespace XmasEngineModel.Management
{

	internal class TriggerManager
	{
		private DictionaryList<Type, Trigger> triggers = new DictionaryList<Type, Trigger>();

		internal event UnaryValueHandler<XmasEvent> EventRaised;

		public void Raise(XmasEvent evt)
		{
			ICollection<Trigger> trigered = triggers.Get(evt.GetType());
			foreach (Trigger t in trigered)
			{
                try
                {

                    if (t.CheckCondition(evt))
                        t.Execute(evt);
                }
                catch (Exception e)
                {
                    if (!(evt is TriggerFailedEvent))
                        this.Raise(new TriggerFailedEvent(t, e));
                }
			}
			var buffer = EventRaised;
			if(buffer != null)
				buffer(this, new UnaryValueEvent<XmasEvent>(evt));
		}

		public void Register(Trigger trigger)
		{
			foreach (Type evt in trigger.Events)
			{
				triggers.Add(evt, trigger);
			}

			if (trigger is MultiTrigger)
				regMulti((MultiTrigger) trigger);
		}

		public void Deregister(Trigger trigger)
		{
			foreach (Type t in trigger.Events)
			{
				triggers.Remove(t, trigger);
			}

			if (trigger is MultiTrigger)
				deregMulti((MultiTrigger) trigger);
		}

		private void regMulti(MultiTrigger trigger)
		{
			trigger.DeregisteredEvent += trigger_DeregisteredEvent;
			trigger.RegisteredEvent += trigger_RegisteredEvent;
		}

		public void deregMulti(MultiTrigger trigger)
		{
			trigger.DeregisteredEvent -= trigger_DeregisteredEvent;
			trigger.RegisteredEvent -= trigger_RegisteredEvent;
		}


		private void trigger_RegisteredEvent(object sender, UnaryValueEvent<Type> evt)
		{
			triggers.Add(evt.Value, (Trigger) sender);
		}


		private void trigger_DeregisteredEvent(object sender, UnaryValueEvent<Type> evt)
		{
			triggers.Remove(evt.Value, (Trigger) sender);
		}
	}
}