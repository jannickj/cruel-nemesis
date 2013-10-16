using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSLibrary.Data;
using Assets.Library.PathFinding;

namespace UnityTestGameTest.TestComponents
{
    public class MockPathFinder : PathFinder<int[,], Point>
    {

        public MockPathFinder(int[,] map)
            : base(map)
        {

        }

        protected override Point[] getNeighbours(Point node)
        {
            Point[] aroundOri = generateOrigoNeighbours();
            Point[] aroundNode = aroundOri.Select(pos => node + pos).ToArray();

            return aroundNode.Where(pos => pos.X >= 0 && pos.X < Map.GetLength(0) && pos.Y >= 0 && pos.Y < Map.GetLength(1) && Map[pos.X, pos.Y] == 0).ToArray(); 
        }

        protected override float getEdgeCost(Point from, Point to)
        {
            return 1;
        }

        protected override float getHeuristic(Point from, Point to)
        {
            return (float)Math.Round(Math.Sqrt(Math.Pow((to.X - from.X), 2) + Math.Pow((to.Y - from.Y), 2)));
        }

        private Point[] generateOrigoNeighbours()
        {
            return new Point[]
            {
                new Point(0,1),
                new Point(0,-1),
                new Point(1,0),
                new Point(-1,0)
            };
        }

        protected override int getPosHash(Point pos)
        {
            return pos.GetHashCode();
        }

        protected override bool isPosEqual(Point pos1, Point pos2)
        {
            return pos1 == pos2;
        }
    }
}
