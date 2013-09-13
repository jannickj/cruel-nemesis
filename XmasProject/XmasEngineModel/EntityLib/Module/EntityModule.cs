using System;
using System.Collections.Generic;
using XmasEngineModel.Management;

namespace XmasEngineModel.EntityLib.Module
{

	/// <summary>
	/// The core module type all modules should be extended from
	/// </summary>
	public abstract class EntityModule : XmasActor
	{
		private XmasEntity entityHost;
		private EntityModule replacedModule;

		/// <summary>
		/// Gets the entity hosting the module
		/// </summary>
		public XmasEntity EntityHost
		{
			get { return entityHost; }
			internal set { entityHost = value; }
		}


		/// <summary>
		/// Gets the module type, this method is meant to be overriden by other modules that wishes to disguise itself as another type
		/// </summary>
		public virtual Type ModuleType { 
			get { return this.GetType (); } 
		}

		/// <summary>
		/// Gets all percepts of the module, this method should be overriden to provide the actual percepts
		/// </summary>
		public virtual IEnumerable<Percept> Percepts
		{
			get { return new Percept[0]; }
		}


		/// <summary>
		/// Attaches the module to an entity
		/// </summary>
		/// <param name="entityHost">The entity host the module is attached to</param>
		/// <param name="replacedModule">The module that the new module replaces, is null if no module was replaced</param>
		internal protected virtual void AttachToEntity(XmasEntity entityHost, EntityModule replacedModule)
		{
			this.entityHost = entityHost;

			if (replacedModule != null && replacedModule.ModuleType == this.ModuleType)
				this.replacedModule = replacedModule;
		}

		/// <summary>
		/// Detaches the module from its host entity.
		/// </summary>
		internal protected virtual void DetachFromEntity()
		{
			if (ReplacedModule != null && ReplacedModule.ModuleType == this.ModuleType)
				entityHost.RegisterModule (ReplacedModule);

		}

		/// <summary>
		/// Gets the ActionManager of its entity host
		/// </summary>
		public override ActionManager ActionManager
		{
			get { return this.entityHost.ActionManager; }
		}

		/// <summary>
		/// Gets the EventManager of its entity host
		/// </summary>
		public override EventManager EventManager
		{
			get { return this.entityHost.EventManager; }
		}

		/// <summary>
		/// Gets the Factory of its entity host
		/// </summary>
		public override XmasFactory Factory
		{
			get { return this.entityHost.Factory; }
		}


		/// <summary>
		/// Gets the world of its entitiy host
		/// </summary>
		public override XmasWorld World
		{
			get { return this.entityHost.World; }
		}

		protected EntityModule ReplacedModule
		{
			get { return replacedModule; }
		}
	}
}