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
using Assets.GameLogic;
using UnityEngine;

namespace Assets.Map
{
	public class StandardGameMapBuilder : TileWorldBuilder
	{
        private const int HEIGHT = 8;
        private const int WIDTH = 6;
        private Player[] players = new Player[2];


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

            this.AddMapOfEntities(map, HEIGHT*2+1, WIDTH*2+1);

            Func<XmasEntity> O = null;
            Func<XmasEntity> F = () => new GruntUnit(players[0]);
            Func<XmasEntity> E = () => new GruntUnit(players[1]);

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
            this.AddMapOfEntities(unitmap, HEIGHT * 2 + 1, WIDTH * 2 + 1);
           
        }


        internal void SetPlayers(Player player, Player player2)
        {
            this.players[0] = player;
            this.players[1] = player2;
        }
    }
}
