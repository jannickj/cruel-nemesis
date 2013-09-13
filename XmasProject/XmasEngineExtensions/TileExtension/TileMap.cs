using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Entities;

namespace XmasEngineExtensions.TileExtension
{
	public class TileMap
	{
		private Size burstSize;
		private Point center;
		private Tile outofmaptile = new Tile();
		private Size size;
		private Tile[,] tiles;

		public TileMap(Size burstSize)
		{
			outofmaptile.AddEntity(new ImpassableWall());
			this.burstSize = burstSize;
			size = new Size(burstSize.Width*2 + 1, burstSize.Height*2 + 1);
			center = new Point(burstSize.Width, burstSize.Height);
			tiles = new Tile[size.Width,size.Height];

			for (int i = 0; i < size.Width; i++)
			{
				for (int j = 0; j < size.Height; j++)
				{
					tiles[i, j] = new Tile();
				}
			}
		}

		public Tile[,] Tiles
		{
			get { return tiles; }
		}

		public Size Size
		{
			get { return size; }
		}

		public Size BurstSize
		{
			get { return this.burstSize; }
		}
		
		public Tile this[int x, int y]
		{
			get
			{
				if (IsOutOfBounds(x, y))
				{
					return outofmaptile;
				}

				return tiles[center.X + x, center.Y - y];
			}
			internal set
			{
				if (!IsOutOfBounds(x, y))
					tiles[center.X + x, center.Y - y] = value;
			}
		}

		public Tile this[Point p]
		{
			get { return this[p.X, p.Y]; }
			internal set { this[p.X, p.Y] = value; }
		}

		public Grid<Tile> this[int x, int y, int range]
		{
			get
			{
				int startx = x - range;
				int starty = y - range;
				int rsize = range*2 + 1;

				Tile[,] r = new Tile[rsize,rsize];

				for (int i = 0; i < rsize; i++)
				{
					for (int j = 0; j < rsize; j++)
					{
						r[i, j] = (Tile)this[i + startx, j + starty].Clone();
					}
				}

				int gridc = rsize/2;

				Grid<Tile> grid = new Grid<Tile>(r, new Point(gridc, gridc));

				return grid;
			}
		}

		public Grid<Tile> this[Point p, int range]
		{
			get { return this [p.X, p.Y, range]; }
		}

		private bool IsOutOfBounds(int x, int y)
		{
			return (x > burstSize.Width || x < -burstSize.Width || y > burstSize.Height || y < -burstSize.Height);
		}
	}
}