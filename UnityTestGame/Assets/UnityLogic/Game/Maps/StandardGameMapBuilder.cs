using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using XmasEngineModel.EntityLib;
using Cruel.Map.Terrain;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic;
using Cruel.GameLogic.Map;
using Assets.UnityLogic.Game.Heroes;

namespace Assets.UnityLogic.Game.Maps
{
    public class StandardGameMapBuilder : GameMapBuilder
	{



        public StandardGameMapBuilder() 
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
            Func<XmasEntity> W = () => new WizardHero(Players[0]);
            Func<XmasEntity> L = () => new WarlordHero(Players[1]);

            Func<XmasEntity>[] unitmap =
				{
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, W, O, O, O, O, O, O, O, O, O, O, O, O, O, L, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, 
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O,  
					O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O, O
				};
            this.AddMapOfEntities(unitmap, DEFAULT_HEIGHT * 2 + 1, DEFAULT_WIDTH * 2 + 1);
        }

    }
}
