using System.Collections.Generic;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management
{

    /// <summary>
    /// The EventManager is meant for raising events on the engine
    /// </summary>
	public class EventManager
	{
		private HashSet<XmasEntity> trackedEntities = new HashSet<XmasEntity>();
		private TriggerManager triggerManager = new TriggerManager();

        /// <summary>
        /// Raises an event, this triggers all triggers with the same event
        /// </summary>
        /// <param name="evt">The event raised</param>
		public void Raise(XmasEvent evt)
		{
			triggerManager.Raise(evt);
		}

        /// <summary>
        /// Adds an entity to be part of the EventManager, so all events raised on that entity is also raised on the eventmanager
        /// </summary>
        /// <param name="xmasEntity">The entity added to the eventmanager</param>
		public void AddEntity(XmasEntity xmasEntity)
		{
			trackedEntities.Add(xmasEntity);

			xmasEntity.TriggerRaised += entity_TriggerRaised;
		}

        /// <summary>
        /// Removes the entity from the EventManager
        /// </summary>
        /// <param name="xmasEntity">The entity to be removed</param>
		public void RemoveEntity(XmasEntity xmasEntity)
		{
			trackedEntities.Remove(xmasEntity);

			xmasEntity.TriggerRaised -= entity_TriggerRaised;
		}

        /// <summary>
        /// Registers a trigger to the EventManager, once registered the trigger will be triggered each time an event is raised on the EventManager that shares an event with the trigger
        /// </summary>
        /// <param name="trigger">The trigger that is registered</param>
		public void Register(Trigger trigger)
		{
			triggerManager.Register(trigger);
		}

        /// <summary>
        /// Deregisters a trigger from the Eventmanager so it will no longer be triggered
        /// </summary>
        /// <param name="trigger">The trigger that is deregistered from the eventmanager</param>
		public void Deregister(Trigger trigger)
		{
			triggerManager.Deregister(trigger);
		}


        /// <summary>
        /// Creates a threadsafe event queue, this queue recieves all events the EventManager raises and stores them in a queue
        /// </summary>
        /// <returns>The ThreadSafe EventQueue</returns>
		public ThreadSafeEventQueue ConstructEventQueue()
		{
			return new ThreadSafeEventQueue(triggerManager);
		}

		#region EVENTS

		private void entity_TriggerRaised(object sender, XmasEvent e)
		{
			Raise(e);
		}

		#endregion
	}
}