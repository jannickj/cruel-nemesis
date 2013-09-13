using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;

namespace XmasEngineModel
{
    /// <summary>
    /// All XmasActors are meant as objects that does things with the engine.
    /// </summary>
	public class XmasActor : XmasObject
	{
		private ActionManager actman;
		private EventManager evtman;
		private XmasFactory factory;
		private XmasWorld world;

		/// <summary>
		/// Gets the world of the engine the Xmas actor is currently part of
		/// </summary>
        /// <exception cref="PropertyIsNullException"></exception>
		public virtual XmasWorld World
		{
			get {
				if (world == null)
					throw new PropertyIsNullException("World", this);
				return world; }
			set { world = value; }
		}

        /// <summary>
        /// Gets the world in form of one of it's extended types
        /// </summary>
        /// <typeparam name="TWorld">The type the world is extended to</typeparam>
        /// <returns>The world</returns>
        /// <exception cref="PropertyIsNullException"></exception>
		public TWorld WorldAs<TWorld>() where TWorld : XmasWorld
		{
			return (TWorld) World;
		}

        /// <summary>
        /// Gets or sets the factory
        /// </summary>
        /// <exception cref="PropertyIsNullException"></exception>
		public virtual XmasFactory Factory
		{
			get 
			{
				if (factory == null)
					throw new PropertyIsNullException("Factory", this);

				return factory; 
			}

			set { factory = value; }
		}

        /// <summary>
        /// Gets the factory in form of one of it's extended types
        /// </summary>
        /// <typeparam name="TFactory">The type the factory is extended to</typeparam>
        /// <returns>the factory</returns>
        /// <exception cref="PropertyIsNullException"></exception>
		public TFactory FactoryAs<TFactory>() where TFactory : XmasFactory
		{
			return (TFactory)Factory;
		}

        /// <summary>
        /// Gets or sets the EventManager of the engine
        /// </summary>
        /// <exception cref="PropertyIsNullException"></exception>
		public virtual EventManager EventManager
		{
			get 
			{
				if (evtman == null)
					throw new PropertyIsNullException("EventManager", this);
				return evtman; 
			}
			set { evtman = value; }
		}

        /// <summary>
        ///  Gets the EventManager in form of one of it's extended types
        /// </summary>
        /// <typeparam name="TEvtman">The type the factory is extended to</typeparam>
        /// <returns>The EventManager</returns>
        /// <exception cref="PropertyIsNullException"></exception>
		public TEvtman EventManagerAs<TEvtman>() where TEvtman : EventManager
		{
			return (TEvtman)EventManager;
		}

        /// <summary>
        /// Gets or sets the ActionManager of the engine
        /// </summary>
        /// <exception cref="PropertyIsNullException"></exception>
		public virtual ActionManager ActionManager
		{
			get
			{
				if (actman == null)
					throw new PropertyIsNullException("ActionManager", this);
				return actman; 
			}
			set { actman = value; }
		}

        /// <summary>
        /// Gets the ActionManager in form of one of it's extended types
        /// </summary>
        /// <typeparam name="TActman">The type the ActionManager is extended to</typeparam>
        /// <returns>the ActionManager</returns>
        /// <exception cref="PropertyIsNullException"></exception>
		public TActman ActionManagerAs<TActman>() where TActman : ActionManager
		{
			return (TActman)ActionManager;
		}

		public override string ToString()
		{
			return this.GetType().Name;
		}

	}
}