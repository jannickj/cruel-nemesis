using System;
using System.Collections.Generic;
using XmasEngineModel.Management;

namespace XmasEngineModel.EntityLib.Module
{

    public abstract class UniversalModule<THost> : UniversalModule where THost : XmasUniversal
    {
        public new THost Host
        {
            get
            {
                return (THost)base.Host;
            }
        }
    }

	/// <summary>
	/// The core module type all modules should be extended from
	/// </summary>
	public abstract class UniversalModule : XmasActor
	{
		private XmasUniversal host;
		private UniversalModule replacedModule;

		/// <summary>
		/// Gets the entity hosting the module
		/// </summary>
        public XmasUniversal Host
		{
			get { return host; }
			internal set { host = value; }
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
		/// <param name="entityHost">The host the module is attached to</param>
		/// <param name="replacedModule">The module that the new module replaces, is null if no module was replaced</param>
		internal protected virtual void AttachTo(XmasUniversal Host, UniversalModule replacedModule)
		{
			this.host = Host;

			if (replacedModule != null && replacedModule.ModuleType == this.ModuleType)
				this.replacedModule = replacedModule;
		}

		/// <summary>
		/// Detaches the module from its host
		/// </summary>
		internal protected virtual void Detach()
		{
			if (ReplacedModule != null && ReplacedModule.ModuleType == this.ModuleType)
				host.RegisterModule (ReplacedModule);

		}

		/// <summary>
		/// Gets the ActionManager of its entity host
		/// </summary>
		public override ActionManager ActionManager
		{
			get { return this.host.ActionManager; }
		}

		/// <summary>
		/// Gets the EventManager of its entity host
		/// </summary>
		public override EventManager EventManager
		{
			get { return this.host.EventManager; }
		}

		/// <summary>
		/// Gets the Factory of its entity host
		/// </summary>
		public override XmasFactory Factory
		{
			get { return this.host.Factory; }
		}


		/// <summary>
		/// Gets the world of its entitiy host
		/// </summary>
		public override XmasWorld World
		{
			get { return this.host.World; }
		}

		protected UniversalModule ReplacedModule
		{
			get { return replacedModule; }
		}

		/// <summary>
		/// Gets the priority of the module, used to determine its place in the module link (higher value = higher priority)
		/// </summary>
		//public virtual int Priority
		//{
		//	get { return 0; }
		//}
	}
}