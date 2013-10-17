using System;
using System.Collections.Generic;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineExtensions.TileExtension.Percepts;
using JSLibrary.Data;
using System.Linq;
using System.Diagnostics;
using XmasEngineModel.Management.Actions;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class VisionModule : UniversalModule<XmasEntity>
	{
		private Vision vision;

		public VisionModule ()
		{
		}

	

		public override IEnumerable<Percept> Percepts {
			get {
				
				UpdateVision();
				
				return vision.VisibleTiles.Select(kv => new TileVisionPercept(kv.Key, kv.Value)).ToArray();
				
			}
		}

		public Vision Vision
		{
			get { return vision; }
		}

		public void UpdateVision()
		{
			vision = this.WorldAs<TileWorld>().View(this.Host);

			//Vision newVision = this.WorldAs<TileWorld>().View(this.EntityHost);
			//IEnumerable<Tile> oldTiles = vision.VisibleTiles.Values;
			//IEnumerable<Tile> newTiles = newVision.VisibleTiles.Values;

			//IEnumerable<Tile> persistentTiles = oldTiles.Intersect (newTiles);

			//TODO: disable listening to event on tile
			//foreach (Tile tile in oldTiles.Except(persistentTiles)) {

			//}

			//TODO: listen to event on tile
			//foreach (Tile tile in newTiles.Except(persistentTiles)) {

			//}

			//vision = newVision;
		}

		private void xmasEntity_UnitMovedPost(UnitMovePostEvent evt)
		{
			UpdateVision ();
		}
	}
}

