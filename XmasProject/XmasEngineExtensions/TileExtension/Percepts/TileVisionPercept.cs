using JSLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
 
namespace XmasEngineExtensions.TileExtension.Percepts
{
	public class TileVisionPercept : Percept
	{
		private Point position;
		private Tile tile;

		public Point Position
		{
			get { return position; }
		}

		public Tile Tile
		{
			get { return tile; }
		}

		public TileVisionPercept(Point position, Tile tile)
		{
			this.position = position;
			this.tile = tile;
		}
	}
}
