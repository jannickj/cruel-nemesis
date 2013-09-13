using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Rule;
using XmasEngineModel.World;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineModel.EntityLib
{
  
	///<summary>
	///     An Entity meant to be added to a XmasWorld
	///</summary>
	public abstract class XmasEntity : XmasActor
	{
        private bool loaded = false;
		private TriggerManager triggers = new TriggerManager();
		internal Dictionary<Type, EntityModule> moduleMap = new Dictionary<Type, EntityModule>();
		

        internal void Load()
        {
            if (loaded == true)
                return;

            loaded = true;
            OnLoad();
        }


		/// <summary>
		/// This method is called when the entity is first loaded into the engine, will only be called once.
		/// </summary>
        protected internal virtual void OnLoad()
        {
			

        }

		/// <summary>
		/// This method is called every time the entity enters the world
		/// </summary>
        protected internal virtual void OnEnterWorld()
        {

        }

		/// <summary>
		/// This method is called every time the entity leaves the world
		/// </summary>
        protected internal virtual void OnLeaveWorld()
        {

        }

		/// <summary>
		/// Checks if the entity has the given module registered
		/// </summary>
		/// <typeparam name="TModule">The module that is checked</typeparam>
		/// <returns>Whether or not the module is registered</returns>
		public bool HasModule<TModule>() where TModule : EntityModule
		{
			return moduleMap.ContainsKey(typeof(TModule));
		}

		/// <summary>
		/// Gets the module requested by type
		/// </summary>
		/// <typeparam name="TModule">Type of the requested module</typeparam>
		/// <returns>the requsted module</returns>
		public TModule Module<TModule> ()
			where TModule : EntityModule
		{
			try {
				return moduleMap [typeof(TModule)] as TModule;
			} catch {
				throw new MissingModuleException(this,typeof(TModule));
			}	
		}

		/// <summary>
		/// Gets the module requested by type and returns it as another type
		/// </summary>
		/// <typeparam name="TModule">Type of the requsted module</typeparam>
		/// <typeparam name="TAlias">The type the module is returned as</typeparam>
		/// <returns>The the requested module in the form of an alias</returns>
		public TAlias ModuleAs<TModule, TAlias>()
			where TModule : EntityModule
			where TAlias : TModule
		{
			return (TAlias) Module<TModule>();
		}


		//
		// Summary:
		//     
		//
		// Parameters:
		//   module:
		//     The module to be added to the entity

		/// <summary>
		/// Registers a module to an entity, if an entity already has the module attached it will override the old module and return it.
		/// </summary>
		/// <param name="module">The module to be added to the entity</param>
		/// <returns>The old module</returns>
		public virtual EntityModule RegisterModule(EntityModule module)
		{
			EntityModule oldModule;
			module.EntityHost = this;

			moduleMap.TryGetValue (module.ModuleType, out oldModule);
			moduleMap [module.ModuleType] = module;

			module.AttachToEntity (this, oldModule);

			return oldModule;
		}

		/// <summary>
		/// Removes the module from the entity, if the module had an older version that module is restored.
		/// </summary>
		/// <param name="module">The module to be removed from the entity's modules</param>
		public virtual void DeregisterModule(EntityModule module)
		{
			if (!moduleMap.ContainsKey(module.ModuleType))
				throw new MissingModuleException(this, module.ModuleType);
			moduleMap.Remove (module.ModuleType);
			module.DetachFromEntity ();
		}

		/// <summary>
		/// Gets the position of the entity, this is done through the world the entity is located in.
		/// </summary>
		public XmasPosition Position
		{
			get { return World.GetEntityPosition(this); }
		}

		internal event EventHandler<XmasEvent> TriggerRaised;

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
		/// Queue an action meant to be performed by the entity onto the entity. This method is threadsafe.
		/// </summary>
		/// <param name="action">The action that is queued</param>
		public void QueueAction(EntityXmasAction action)
		{
			action.Source = this;
			ActionManager.QueueAction(action);
		}

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
		/// Creates a threadsafe event queue, this queue recieves all events the entity raises and stores them in a queue
		/// </summary>
		/// <returns>The ThreadSafe EventQueue</returns>
		public ThreadSafeEventQueue ConstructEventQueue()
		{
			return new ThreadSafeEventQueue(triggers);
		}

		public override string ToString()
		{
			string basestr = string.Format("{0} [{1}]", GetType().Name, Id);
			try
			{
				return string.Format("{0} at {1}", basestr, Position);
			}
			catch
			{
				return basestr;
			}
		}
	}
}