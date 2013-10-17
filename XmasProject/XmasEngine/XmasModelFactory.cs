using System;
using XmasEngineController;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
    /// <summary>
    /// Factory responsible for constructing all major components of the Engine's model core
    /// </summary>
	public class XmasModelFactory
	{
        /// <summary>
        /// Constructs a engine with all its components
        /// </summary>
        /// <param name="builder">The builder for constructing the world</param>
        /// <returns>The model</returns>
		public virtual XmasModel ConstructModel(XmasWorldBuilder builder)
		{
			EventManager evtman = ConstructEventManager();
			ActionManager actman = ConstructActionManager(evtman); 
			XmasFactory fact = ConstructFactory(actman);
			XmasModel engine = new XmasModel(builder, actman, evtman, fact);

			return engine;
		}

        /// <summary>
        /// Constructs the factory used inside the engine
        /// </summary>
        /// <param name="actman">The action manager of the engine</param>
        /// <returns></returns>
		protected virtual XmasFactory ConstructFactory(ActionManager actman)
		{
			return new XmasFactory(actman);
		}

        /// <summary>
        /// Constructs the EventManager of the engine
        /// </summary>
        /// <returns>the eventmanager</returns>
		protected virtual EventManager ConstructEventManager()
		{
			return new EventManager();
		}

        /// <summary>
        /// Constructs the ActionManager of the engine
        /// </summary>
        /// <param name="evtman">The EventManager of the engine</param>
        /// <returns>The ActionManager</returns>
		protected virtual ActionManager ConstructActionManager(EventManager evtman)
		{
			return new ActionManager(evtman);
		}

	}
}