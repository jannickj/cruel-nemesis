using System;
using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;
using XmasEngineModel.World;

namespace XmasEngineModel
{

    /// <summary>
    /// Uses this builder to explain how a world implementation should be constructed
    /// </summary>
	public abstract class XmasWorldBuilder
	{
		private List<Func<XmasAction>> buildactions = new List<Func<XmasAction>>();

		
        /// <summary>
        /// Stores information on how an entity should be added when the world is being built
        /// </summary>
        /// <param name="ent">The entity constructor for the entity to be added</param>
        /// <param name="info">Information on how to add the entity</param>
		public void AddEntity(Func<XmasEntity> ent, EntitySpawnInformation info)
		{
			buildactions.Add(() => new AddEntityAction(ent(), info));
		}

        /// <summary>
        /// Override this for explaining to the engine on how to construct the world
        /// </summary>
        /// <returns>The world the engine will use</returns>
		protected abstract XmasWorld ConstructWorld ();

        /// <summary>
        /// Builds a full world along with all entities added to it
        /// </summary>
        /// <param name="actman">The action manager of the engine</param>
        /// <returns>The fully built world</returns>
		public XmasWorld Build()
		{
			return ConstructWorld ();
		}

        internal void PopulateWorld(ActionManager actman)
        {
            foreach (Func<XmasAction> buildaction in buildactions.ToArray())
            {
                actman.QueueAction(buildaction());
            }

        }
	}
}