using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineExtensions.TileExtension.Percepts;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace XmasEngineExtensions.TileExtension
{
	public class TileWorld : XmasWorld
	{
		private Dictionary<XmasEntity, Point> entlocs = new Dictionary<XmasEntity, Point>();
		private TileMap map;


		public TileWorld(Size burstSize)
		{
			this.map = new TileMap(burstSize);			
		}

		public TileWorld(TileMap map)
		{
			this.map = map;
		}

		public Size Size
		{
			get { return map.Size; }
		}

		public Size BurstSize
		{
			get { return this.map.BurstSize; }
		}

		public Vision View(Point p, int range, XmasEntity xmasEntity)
		{
			return new Vision(map[p.X, p.Y, range], xmasEntity);
		}

		public Vision View(int range, XmasEntity xmasEntity)
		{
			return View(entlocs[xmasEntity], range, xmasEntity);
		}

		public Vision View(XmasEntity xmasEntity)
		{
			return View(xmasEntity.Module<VisionRangeModule>().VisionRange, xmasEntity);
		}

		protected override bool OnAddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
			TilePosition tilePos = (TilePosition) info.Position;
			return AddEntity(xmasEntity, tilePos);
		}
		
		private bool AddEntity(XmasEntity xmasEntity, TilePosition pos)
		{
			Point point = pos.Point;
			
			Tile tile = map[point.X, point.Y];

			if (!tile.CanContain(xmasEntity))
				return false;

			entlocs[xmasEntity] = point;
			tile.AddEntity(xmasEntity);
			return true;
		}
		
		public override XmasPosition GetEntityPosition(XmasEntity xmasEntity)
		{
			return new TilePosition(entlocs[xmasEntity]);
		}
		
		public override bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition)
		{
			return SetEntityPosition (xmasEntity, (TilePosition)tilePosition);
		}
		
		private bool SetEntityPosition(XmasEntity xmasEntity, TilePosition pos)
		{
			Point oldPoint;
			bool entityExistsInMap = entlocs.TryGetValue(xmasEntity, out oldPoint);
			
			
			if (!AddEntity (xmasEntity, pos))
				return false;
			
			if (entityExistsInMap)
				map [oldPoint].RemoveEntity (xmasEntity);
			
			return true;
		}

		public override ICollection<XmasEntity> GetEntities (XmasPosition pos)
		{
			Point p = ((TilePosition)pos).Point;
			return map [p].Entities;
		}

		protected override void OnRemoveEntity (XmasEntity entity)
		{
			map [entlocs [entity]].RemoveEntity (entity);
			entlocs.Remove (entity);
		}
	}
}