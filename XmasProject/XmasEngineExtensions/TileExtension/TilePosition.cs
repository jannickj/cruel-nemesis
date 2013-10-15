using JSLibrary.Data;
using XmasEngineModel.World;

namespace XmasEngineExtensions.TileExtension
{
	public class TilePosition : XmasPosition
	{
		private Point point;

		public TilePosition(Point p)
		{
			point = p;
		}

		public Point Point
		{
			get { return point; }
		}

		public override string ToString ()
		{
			return Point.ToString();
		}

        public override EntitySpawnInformation GenerateSpawn()
        {
            return new TileSpawnInformation(new TilePosition(point));
        }

        public override bool Equals(object obj)
        {
            TilePosition tpos = obj as TilePosition;
            if (tpos != null)
                return point == tpos.point;
            else
                return false;
        }
	}
}