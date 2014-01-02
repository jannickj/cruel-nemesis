using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using Cruel.Map.Terrain;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Map;
using CruelGameData.GameLogic.Game.Unit;

namespace CruelGameData.GameLogic.Game.Maps
{
    public class TestMapBuilder : GameMapBuilder
    {

        public TestMapBuilder()
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

            this.AddMapOfEntities(map, DEFAULT_HEIGHT * 2 + 1, DEFAULT_WIDTH * 2 + 1);

            Func<XmasEntity> O = null;
            Func<XmasEntity> F = () => new GruntUnit(Players[0]);
            Func<XmasEntity> E = () => new GruntUnit(Players[1]);

            Func<XmasEntity>[] unitmap =
				{
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, F, O, O, O, O, O, O, O, O, O, O, O, O, O, E, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, F, O, O, O, O, O, O, O, O, O, O, O, O, O, E, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, F, O, O, O, O, O, O, O, O, O, O, O, O, O, E, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, F, O, O, O, O, O, O, O, O, O, O, O, O, O, E, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O
				};
            this.AddMapOfEntities(unitmap, DEFAULT_HEIGHT * 2 + 1, DEFAULT_WIDTH * 2 + 1);
        }
    }
}
