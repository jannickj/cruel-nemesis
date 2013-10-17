using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;

namespace XmasEngineModel.EntityLib
{
    public class XmasUniversal : XmasActor
    {
        private TriggerManager triggers = new TriggerManager();
        internal Dictionary<Type, UniversalModule> moduleMap = new Dictionary<Type, UniversalModule>();
        internal event EventHandler<XmasEvent> TriggerRaised;

        /// <summary>
        /// Raises an event on the entity, this will call all triggers with the same event that are registered to the entity
        /// </summary>
        /// <param name="evt">Event to be raised</param>
        public void Raise(XmasEvent evt)
        {
            lock (this)
            {
                triggers.Raise(evt);
                if (TriggerRaised != null)
                    TriggerRaised(this, evt);
            }
        }


        /// <summary>
        /// Registers a module to an entity, if an entity already has the module attached it will override the old module and return it.
        /// </summary>
        /// <param name="module">The module to be added to the entity</param>
        /// <returns>The old module</returns>
        public virtual UniversalModule RegisterModule(UniversalModule module)
        {
            UniversalModule oldModule;
            module.Host = this;

            moduleMap.TryGetValue(module.ModuleType, out oldModule);
            moduleMap[module.ModuleType] = module;

            module.AttachTo(this, oldModule);

            return oldModule;
        }

        /// <summary>
        /// Removes the module from the entity, if the module had an older version that module is restored.
        /// </summary>
        /// <param name="module">The module to be removed from the entity's modules</param>
        public virtual void DeregisterModule(UniversalModule module)
        {
            if (!moduleMap.ContainsKey(module.ModuleType))
                throw new MissingModuleException(this, module.ModuleType);
            moduleMap.Remove(module.ModuleType);
            module.Detach();
        }

        /// <summary>
        /// Creates a threadsafe event queue, this queue recieves all events the entity raises and stores them in a queue
        /// </summary>
        /// <returns>The ThreadSafe EventQueue</returns>
        public ThreadSafeEventQueue ConstructEventQueue()
        {
            return new ThreadSafeEventQueue(triggers);
        }

        /// <summary>
        /// Registers a trigger to the entity, which the entity will call when the triggers events are raised.
        /// This method is threadsafe.
        /// </summary>
        /// <param name="trigger">The trigger to be registered</param>
        public void Register(Trigger trigger)
        {
            lock (this)
            {
                triggers.Register(trigger);
            }
        }

        /// <summary>
        /// Deregisters a trigger from the entity, the trigger will no longer called when the triggers events are raised.
        /// This method is threadsafe.
        /// </summary>
        /// <param name="trigger">The trigger to be deregistered</param>
        public void Deregister(Trigger trigger)
        {
            lock (this)
            {
                triggers.Deregister(trigger);
            }
        }


        /// <summary>
        /// Checks if the entity has the given module registered
        /// </summary>
        /// <typeparam name="TModule">The module that is checked</typeparam>
        /// <returns>Whether or not the module is registered</returns>
        public bool HasModule<TModule>() where TModule : UniversalModule
        {
            return moduleMap.ContainsKey(typeof(TModule));
        }

        /// <summary>
        /// Gets the module requested by type
        /// </summary>
        /// <typeparam name="TModule">Type of the requested module</typeparam>
        /// <returns>the requsted module</returns>
        public TModule Module<TModule>()
            where TModule : UniversalModule
        {
            try
            {
                return moduleMap[typeof(TModule)] as TModule;
            }
            catch
            {
                throw new MissingModuleException(this, typeof(TModule));
            }
        }

        /// <summary>
        /// Gets the module requested by type and returns it as another type
        /// </summary>
        /// <typeparam name="TModule">Type of the requsted module</typeparam>
        /// <typeparam name="TAlias">The type the module is returned as</typeparam>
        /// <returns>The the requested module in the form of an alias</returns>
        public TAlias ModuleAs<TModule, TAlias>()
            where TModule : UniversalModule
            where TAlias : TModule
        {
            return (TAlias)Module<TModule>();
        }

    }
}
