using System;
namespace JSLibrary.Data
{
	public struct Point
	{
		private int x, y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Point(Point p, Vector v)
		{
			x = p.X + v.X;
			y = p.Y + v.Y;
		}

		public int X
		{
			get { return x; }
		}

		public int Y
		{
			get { return y; }
		}

        public override bool Equals(Object obj)
        {
            return obj is Point && this == (Point)obj;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

		public static Point operator +(Point p1, Point p2)
		{
			return new Point(p1.X + p2.X, p1.Y + p2.Y);
		}

		public static Point operator -(Point p1, Point p2)
		{
			return new Point(p1.X - p2.X, p1.Y - p2.Y);
		}

		public static Point operator +(Point p, Vector v)
		{
			return new Point(p.X + v.X, p.Y + v.Y);
		}

		public static Point operator +(Vector v, Point p)
		{
			return p + v;
		}

		public static Point operator -(Point p, Vector v)
		{
			return new Point(p.X - v.X, p.Y - v.Y);
		}

		public static Point operator -(Vector v, Point p)
		{
			return p - v;
		}

		public static Point operator *(Point p, Vector v)
		{
			return new Point (p.X * v.X, p.Y * v.Y);
		}

		public override string ToString()
		{
			return string.Format("({0},{1})", X, Y);
		}
	}
}