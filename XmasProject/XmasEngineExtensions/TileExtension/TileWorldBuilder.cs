using System;
using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions.TileExtension
{
	public class TileWorldBuilder : XmasWorldBuilder
	{
		private Size size;

		public Size Size
		{
			get { return size; }
		}

		public void AddEntity(Func<XmasEntity> entity, Point p)
		{
			this.AddEntity(entity, new TileSpawnInformation(new TilePosition(p)));
		}

		public TileWorldBuilder(Size size)
		{
			this.size = size;
		}

		#region implemented abstract members of XmasWorldBuilder

		protected override XmasWorld ConstructWorld ()
		{
			return new TileWorld (Size);
		}

		#endregion

		private IEnumerable<Point> TilesInChunk(Point start, Point stop, ICollection<Point> exceptions)
		{
			Point min = new Point(Math.Min(start.X, stop.X), Math.Min(start.Y, stop.Y));
			Point max = new Point(Math.Max(start.X, stop.X), Math.Max(start.Y, stop.Y));

			for (int x = min.X; x <= max.X; x++)
			{
				for (int y = min.Y; y <= max.Y; y++)
				{
					if (exceptions == null || !exceptions.Contains(new Point(x, y)))
						yield return new Point(x, y);
				}
			}
		}

		public void AddChunk (Func<XmasEntity> constructEntity, Point start, Point stop)
		{
			AddChunkExcept(constructEntity, start, stop, null);
		}

		public void AddChunkExcept (Func<XmasEntity> constructEntity, Point start, Point stop, ICollection<Point> exceptions)
		{
			foreach (Point p in TilesInChunk(start, stop, exceptions))
				AddEntity(constructEntity, p);
		}

		public void AddCollection (Func<XmasEntity> constructEntity, IEnumerable<Point> points)
		{
			foreach (Point p in points)
				AddEntity (constructEntity, p);
		}


		public void AddMapOfEntities(Func<XmasEntity>[] ctormap, int width, int height)
		{
			int offx = this.Size.Width;
			int offy = this.Size.Height;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Func<XmasEntity> c = ctormap[x + y * width];

					int realx = x - offx;
					int realy = offy - y;
					if (c != null)
						this.AddEntity(c, new Point(realx, realy));
				}
			}
		}
		
//		public void RemoveChunk<TEntity>(Point start, Point stop)
//			where TEntity : Entity, new()
//		{
//			RemoveChunkExcept<TEntity>(start, stop, null);
//		}

//		public void RemoveChunkExcept<TEntity>(Point start, Point stop, ICollection<Point> exceptions)
//			where TEntity : Entity, new()
//		{
//			foreach (Point p in TilesInChunk (start, stop, exceptions)) {
//				pointToEntities.
//			}
//
////			foreach (Tile tile in TilesInChunk(start, stop, exceptions).Select(p => this.map[p.X,p.Y]))
////			{
////				foreach (TEntity entity in tile.Entities.OfType<TEntity>().ToArray())
////					this.OnRemoveEntity(entity);
////			}
//		}
	}
}