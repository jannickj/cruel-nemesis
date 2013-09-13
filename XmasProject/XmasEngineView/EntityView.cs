using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace XmasEngineView
{
    /// <summary>
    /// A view for a specific entity
    /// </summary>
	public abstract class EntityView : IDisposable
	{
		protected ThreadSafeEventQueue eventqueue;
		protected XmasEntity model;

        /// <summary>
        /// Constructs a new EntityView
        /// </summary>
        /// <param name="model">The entity that view is meant to show</param>
        /// <param name="position">The position of the entity</param>
        /// <param name="tman">The ThreadSafeEventManager that is controlled the XmasView</param>
        public EntityView(XmasEntity model, XmasPosition position, ThreadSafeEventManager tman)
		{
			this.model = model;
            this.Position = position;
			eventqueue = model.ConstructEventQueue();
			tman.AddEventQueue(eventqueue);
		}

        /// <summary>
        /// Gets the EventQueue directly tied to a specific entity
        /// </summary>
		public ThreadSafeEventQueue EventQueue
		{
			get { return eventqueue; }
		}

        /// <summary>
        /// Gets the entity of the view
        /// </summary>
		public XmasEntity Model
		{
			get { return model; }
		}

        /// <summary>
        /// Gets the position of where the view last know the model to be
        /// </summary>
		public abstract XmasPosition Position { get; protected set; }

		public virtual void Dispose()
		{
			eventqueue.Dispose();
		}
	}
}