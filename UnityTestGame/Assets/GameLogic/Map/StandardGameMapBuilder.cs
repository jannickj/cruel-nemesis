using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using XmasEngineModel.EntityLib;
using Assets.Map.Terrain;
using Assets.GameLogic.Unit;

namespace Assets.Map
{
	public class StandardGameMapBuilder : TileWorldBuilder
	{
        private const int HEIGHT = 8;
        private const int WIDTH = 6;

        public StandardGameMapBuilder() : base(new Size(HEIGHT,WIDTH))
        {
            Func<XmasEntity> D = () => new TerrainEntity(TerrainTypes.Default);

            Func<XmasEntity>[] map =
				{
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D,  
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D,  
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D
				};

            this.AddMapOfEntities(map, 17, 13);
            this.AddEntity(new GruntUnit(), new Point(1, 2));

            this.AddEntity(new GruntUnit(), new Point(0, 0));
        }
        
    }
}
