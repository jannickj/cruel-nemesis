using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension;
using Cruel.Library.PathFinding;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Entities;

namespace Cruel.GameLogic.Map
{
	public class TilePathFinder : PathFinder<TileWorld,TilePosition>
	{

        public TilePathFinder(TileWorld map) : base(map)
        {

        }

        protected override TilePosition[] getNeighbours(TilePosition node)
        {            
            Point[] aroundOri = generateOrigoNeighbours();
            TilePosition[] aroundNode = aroundOri.Select(pos => new TilePosition(node.Point + pos)).ToArray();

            return aroundNode.Where(pos => !this.Map.GetEntities(pos).Any(ent => ent is ImpassableWall)).ToArray();    
        }

        protected override float getEdgeCost(TilePosition from, TilePosition to)
        {
            return 1f;
        }

        protected override float getHeuristic(TilePosition from, TilePosition to)
        {
            Point toP = to.Point;
            Point fromP = from.Point;
            double val = calcDist(fromP, toP);
            return (float)val;
        }

        private Point[] generateOrigoNeighbours()
        {
            return new Point[]
            {                
                
                new Point(1,1),
                new Point(-1,-1),
                new Point(-1,1),
                new Point(1,-1),
                new Point(0,1),
                new Point(0,-1),
                new Point(1,0),
                new Point(-1,0)
                
            };
        }

        private double calcDist(Point p1, Point p2)
        {
            return Math.Round(Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2))); ;
        }

        protected override int getPosHash(TilePosition pos)
        {
            return pos.Point.GetHashCode();
        }

        protected override bool isPosEqual(TilePosition pos1, TilePosition pos2)
        {
            return pos1.Point == pos2.Point;
        }
    }
}
