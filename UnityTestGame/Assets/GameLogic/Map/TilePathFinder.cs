using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension;
using Assets.Library.PathFinding;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Entities;

namespace Assets.GameLogic.Map
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

        protected override int getEdgeCost(TilePosition from, TilePosition to)
        {
            return 1;
        }

        protected override int getHeuristic(TilePosition from, TilePosition to)
        {
            Point toP = to.Point;
            Point fromP = from.Point;
            return (int)Math.Round(Math.Sqrt((toP.X - fromP.X) ^ 2 + (toP.Y - fromP.Y) ^ 2));
        }

        private Point[] generateOrigoNeighbours()
        {
            return new Point[]
            {
                new Point(0,1),
                new Point(0,-1),
                new Point(1,0),
                new Point(-1,0),
                new Point(1,1),
                new Point(-1,-1),
                new Point(-1,1),
                new Point(1,-1)
            };
        }
    }
}
