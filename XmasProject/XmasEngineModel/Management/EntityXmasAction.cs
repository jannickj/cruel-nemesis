using System;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management
{

    /// <summary>
    /// An abstract action meant to be queued on entities
    /// </summary>
	public abstract class EntityXmasAction : XmasAction
	{
		private XmasEntity source;

        /// <summary>
        /// Gets the entity the action is executed by
        /// </summary>
		public XmasEntity Source
		{
			get { return source; }
			internal set { source = value; }
		}
	
	}

    /// <summary>
    /// An abstract action meant to be queued on a specific type of entity
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the action is meant to be queued onto</typeparam>
	public abstract class EntityXmasAction<TEntity> : EntityXmasAction where TEntity : XmasEntity
	{

        /// <summary>
        /// Gets the entity the action is executed by
        /// </summary>
		public new TEntity Source
		{
			get { return (TEntity) base.Source; }
		}


	}
}