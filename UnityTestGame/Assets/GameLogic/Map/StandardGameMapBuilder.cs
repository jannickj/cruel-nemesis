using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using XmasEngineModel.EntityLib;
using Assets.Map.Terrain;

namespace Assets.Map
{
	public class StandardGameMapBuilder : TileWorldBuilder
	{
        private const int HEIGHT = 18;
        private const int WIDTH = 14;

        public StandardGameMapBuilder() : base(new Size(HEIGHT,WIDTH))
        {
            Func<XmasEntity> D = () => new TerrainEntity(TerrainTypes.Default);

            Func<XmasEntity>[] map =
				{
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, 
					D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D, D
				};

            this.AddMapOfEntities(map, WIDTH, HEIGHT);
        }
        
    }
}
