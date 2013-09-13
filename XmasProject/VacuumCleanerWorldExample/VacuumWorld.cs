using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace VacuumCleanerWorldExample
{
	public class VacuumWorld : XmasWorld
	{
		//The two tiles that can contain the vacuum cleaner
		private VacuumCleanerAgent[] vacuumTiles = new VacuumCleanerAgent[2];

		//these two tiles contain the dirt
		private DirtEntity[] dirtTiles = new DirtEntity[2];
		
		//Override this to provide a way to insert the agent
		protected override bool OnAddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
			var spawn = (VacuumSpawnInformation)info;
			var spawnloc = (VacuumPosition)spawn.Position;
			
			if (xmasEntity is VacuumCleanerAgent)
			{
				vacuumTiles[spawnloc.PosID] = xmasEntity as VacuumCleanerAgent;
				return true;
			}
			else if (xmasEntity is DirtEntity)
			{
				dirtTiles[spawnloc.PosID] = xmasEntity as DirtEntity;
				return true;
			}

			return false;
		}

		//this method provides the world the ability to remove entities
		protected override void OnRemoveEntity(XmasEntity entity)
		{
			//since the vacuum cant be removed there is no need to provide this in the world
			if (entity is DirtEntity)
			{
				var vpos = (VacuumPosition) this.GetEntityPosition(entity);
				dirtTiles[vpos.PosID] = null;
			}
		}

		//override this for the world to provide the location of the vacuum cleaner
		public override XmasPosition GetEntityPosition(XmasEntity xmasEntity)
		{
			//Go through each tile to see if the agent is contained within if not return the posion of -1
			if (vacuumTiles[0] == xmasEntity)
				return new VacuumPosition(0);
			else if (vacuumTiles[1] == xmasEntity)
				return new VacuumPosition(1);
			
			//same is done for dirt
			else if (dirtTiles[0] == xmasEntity)
				return new VacuumPosition(0);
			else if (dirtTiles[1] == xmasEntity)
				return new VacuumPosition(1);
			else
				return new VacuumPosition(-1);
		}

		//Override this to provide the ability for the world to change position of the entity
		public override bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition)
		{
			//This can be implemented by removing the entity from its last location and re-add it to the tile of its new position
			var pos = (VacuumPosition) tilePosition;
			var lastpos = (VacuumPosition)this.GetEntityPosition(xmasEntity);
			if (xmasEntity is VacuumCleanerAgent)
			{
				this.vacuumTiles[lastpos.PosID] = null;
				this.vacuumTiles[pos.PosID] = xmasEntity as VacuumCleanerAgent;
				return true;
			}
			else if (xmasEntity is DirtEntity)
			{
				this.dirtTiles[lastpos.PosID] = null;
				this.dirtTiles[pos.PosID] = xmasEntity as DirtEntity;
				return true;
			}
			return false;
			
		}


		//Override this to get all entities on the world at a specific location
		public override ICollection<XmasEntity> GetEntities(XmasPosition pos)
		{
			var vpos = (VacuumPosition)pos;

			//check if the vacuum cleaner is located at the position then return the entity, if not give an empty collection
			XmasEntity[] vacuum;
			
			if (this.vacuumTiles[vpos.PosID] != null)
				vacuum = new XmasEntity[] { this.vacuumTiles[vpos.PosID] };
			else
				vacuum = new XmasEntity[0];

			//Check if dirt is located on the given position
			XmasEntity[] dirt;

			if (this.dirtTiles[vpos.PosID] != null)
				dirt = new XmasEntity[] { this.dirtTiles[vpos.PosID] };
			else
				dirt = new XmasEntity[0];

			//Concatenate the two collections of dirt and vacuum cleaner and make them into an array for the data to be immutable
			return vacuum.Concat(dirt).ToArray();
		}
	}
}
