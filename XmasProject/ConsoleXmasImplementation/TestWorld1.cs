using ConsoleXmasImplementation.Model.Entities;
using JSLibrary.Data;
using System;
using System.Collections.Generic;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation
{
	public class TestWorld1 : TileWorldBuilder
	{
		public TestWorld1() : base(new Size(10, 10))
		{
			BuildMap();
		}

		private void BuildMap()
		{
			Func<XmasEntity> W = () => new Wall();
			Func<XmasEntity> O = null;
            Func<XmasEntity> D = () => new DropZone();
            Func<XmasEntity> X = () => new Package();

			Func<XmasEntity>[] map =
				{
					O, O, O, O, O, O, O, W, O, O, O, O, O, W, O, O, O, W, O, D, O,
					O, W, W, W, W, O, W, W, W, W, W, W, O, W, O, W, O, W, O, W, X,
					O, W, O, X, O, O, O, W, O, O, O, W, O, W, W, W, O, W, O, W, O,
					O, W, O, W, W, W, O, W, O, W, O, O, O, W, O, O, O, W, O, W, O,
					O, W, O, O, O, W, O, O, O, W, W, W, O, W, O, W, O, O, O, W, O,
					O, W, W, W, O, W, W, W, W, W, O, W, O, W, O, W, W, W, W, W, X,
					O, W, O, O, O, W, O, O, O, O, O, W, O, O, O, O, O, O, O, O, O,
					O, O, O, W, O, O, O, W, W, O, W, W, O, W, W, W, W, W, O, W, W,
					O, W, O, W, O, W, O, W, O, O, O, W, O, W, O, O, O, W, O, W, O,
					O, W, W, W, W, W, O, W, O, W, O, O, O, O, O, W, O, W, O, W, O,
					O, W, O, O, O, O, O, W, O, W, W, W, W, W, O, W, O, W, O, W, O,
					O, W, W, W, O, W, W, W, O, W, O, W, O, W, O, W, O, W, O, O, O,
					O, O, O, W, O, W, O, O, O, O, O, O, O, W, W, W, O, W, W, W, W,
					O, W, W, W, O, W, W, W, W, W, W, W, O, O, W, O, O, O, O, O, O,
					O, W, O, W, O, W, O, O, O, O, O, W, W, O, W, O, W, W, W, W, O,
					O, W, O, W, O, W, O, W, W, W, O, W, O, O, W, O, O, O, O, W, O,
					O, O, O, O, O, O, O, W, O, W, O, O, O, W, W, O, W, W, O, W, O,
					W, W, W, W, W, W, O, W, O, W, O, W, O, W, O, X, O, W, O, W, W,
					O, W, O, O, O, W, O, O, O, W, O, W, O, W, O, W, O, W, O, X, O,
					O, W, O, W, O, W, W, W, W, W, O, W, O, W, W, W, O, W, W, W, O,
					O, O, O, W, O, O, O, X, O, O, O, W, O, O, O, O, O, O, O, O, O

				};
			
			this.AddMapOfEntities(map,21,21);

			this.AddEntity(() =>new Player(), new Point(-10, -8));
            //this.AddEntity(new Ghost("testname"), new Point(10, 9));
			//this.AddEntity(new Package(), new Point(10, 10));
			this.AddEntity(() => new GrabberAgent("grabber"), new Point(10, 10));
			//this.AddEntity(new GrabberAgent("grabber2"), new Point(-10, -10));
			//this.AddEntity(new DropZone(), new Point(9, 10));
		}

		public IEnumerable<int> AlternateRange(int start, int end, int inc)
		{
			for (int i = start; i <= end; i += inc)
			{
				yield return i;
			}
		}

		public void AddMapOfEntities(Func<XmasEntity>[] ctormap, int width, int height)
		{
			int offx = this.Size.Width;
			int offy = this.Size.Height;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Func<XmasEntity> c = ctormap[x + y*width];

					int realx = x - offx;
					int realy =offy - y;
					if(c!=null)
						this.AddEntity(c, new Point(realx,realy));
				}
			}
		}

		public void AddWall(int x, int y)
		{
			this.AddEntity(() => new Wall(), new Point(x, y));
		}

		public void AddWall(int sx, int sy, int ex, int ey)
		{
			this.AddChunk(() => new Wall(), new Point(sx,sy), new Point(ex,ey) );
		}

		public void AddWall(int sx, int sy, int ex, int ey, params  Point[] except)
		{
			this.AddChunkExcept(() => new Wall(), new Point(sx, sy), new Point(ex, ey), except);
		}
	}
}