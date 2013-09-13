using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineModel.World;

namespace XmasEngineModel
{

    /// <summary>
    /// The world used in the engine, all worlds that are to be used in the engine should extend XmasWorld
    /// </summary>
	public abstract class XmasWorld
	{
		private ulong nextId = 1;
		private Dictionary<string,Agent> agentLookup = new Dictionary<string, Agent>();
		private Dictionary<ulong,XmasEntity> entityLookup = new Dictionary<ulong, XmasEntity>();
		private EventManager evtman;

        /// <summary>
        /// Gets or sets the EventManager of the engine
        /// </summary>
		public EventManager EventManager
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
        /// Adds an Entity to be part of the engine and the world
        /// </summary>
        /// <param name="xmasEntity">The entity added to world</param>
        /// <param name="info">Information regarding where and how to add the entity</param>
        /// <returns>whether or not the adding of the entity was successful</returns>
		public bool AddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
            xmasEntity.Load();
			if (xmasEntity is Agent)
			{
				Agent agent = xmasEntity as Agent;
				Agent otheragent;
				if(string.IsNullOrEmpty(agent.Name))
					throw new AgentHasNoNameException(agent);
				else if (agentLookup.TryGetValue(agent.Name, out otheragent))
				{
					throw new AgentAlreadyExistsException(agent, otheragent);
				}
				else
				{
					agentLookup.Add(agent.Name,agent);
				}
			}

			bool entityadded = OnAddEntity(xmasEntity, info);
			if (entityadded)
			{
          

				xmasEntity.Id = nextId;
				this.entityLookup.Add(xmasEntity.Id,xmasEntity);
				nextId++;

				EventManager.Raise(new EntityAddedEvent(xmasEntity,info.Position));

                this.evtman.AddEntity(xmasEntity);
                xmasEntity.OnEnterWorld();
			}

			return entityadded;
		}

        /// <summary>
        /// Removes an entity from the engine
        /// </summary>
        /// <param name="entity">the entity to be removed</param>
		public void RemoveEntity(XmasEntity entity)
		{
			if (entity is Agent) {
				Agent agent = entity as Agent;

				agentLookup.Remove(agent.Name);
			}

			entityLookup.Remove (entity.Id);

			OnRemoveEntity(entity);

			EventManager.Raise(new EntityRemovedEvent(entity));

            this.evtman.RemoveEntity(entity);
            entity.OnLeaveWorld();

		}

        /// <summary>
        /// Override this method to intercept when a entity is added
        /// </summary>
        /// <param name="xmasEntity">The entity to be added</param>
        /// <param name="info">information of the entity</param>
        /// <returns>Whether the entity could be added</returns>
		protected abstract bool OnAddEntity(XmasEntity xmasEntity, EntitySpawnInformation info);

        /// <summary>
        /// Override this method to intercept when a entity is being removed
        /// </summary>
        /// <param name="entity">The entity that is being removed</param>
		protected abstract void OnRemoveEntity(XmasEntity entity);

        /// <summary>
        /// Gets the position that a entity is currently at
        /// </summary>
        /// <param name="xmasEntity">The entity which position is located</param>
        /// <returns>The position of the entity</returns>
		public abstract XmasPosition GetEntityPosition(XmasEntity xmasEntity);

        /// <summary>
        /// Sets the position of an entity
        /// </summary>
        /// <param name="xmasEntity">The entity which position is being set</param>
        /// <param name="tilePosition">The position where the entity is set</param>
        /// <returns>Whether or not it could be succesfully set at that position</returns>
		public abstract bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition);

        /// <summary>
        /// Attempts to locate an agent through a certain name
        /// </summary>
        /// <param name="name">The name of the agent</param>
        /// <param name="agent">The agent that is being located</param>
        /// <returns>Whether or not the agent could be located</returns>
		public bool TryGetAgent(string name, out Agent agent)
		{
			return this.agentLookup.TryGetValue(name, out agent);
		}

        /// <summary>
        /// Gets a collection of entities which are located at a certain position
        /// </summary>
        /// <param name="pos">The position where the entities are retrieved from</param>
        /// <returns>a collection of entities</returns>
		public abstract ICollection<XmasEntity> GetEntities (XmasPosition pos);

		public override string ToString()
		{
			return this.GetType().Name;
		}
	}
}