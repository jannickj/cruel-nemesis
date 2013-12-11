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

namespace Cruel.GameLogic.Map
{
	public class GameMapBuilder : TileWorldBuilder
	{
        public const int DEFAULT_HEIGHT = 8;
        public const int DEFAULT_WIDTH = 6;
        private Player[] players = new Player[2];

        public Player[] Players
        {
            get { return players; }
        }


        public GameMapBuilder() : base(new Size(DEFAULT_HEIGHT,DEFAULT_WIDTH))
        {
            
           
        }

        public GameMapBuilder(Size size)
            : base(size)
        {
        }


        public void SetPlayers(Player player, Player player2)
        {
            this.players[0] = player;
            this.players[1] = player2;
        }
    }
}
