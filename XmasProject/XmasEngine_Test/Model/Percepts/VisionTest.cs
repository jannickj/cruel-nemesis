using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data;
using NUnit.Framework;
using XmasEngine_Test.ExampleObjects;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineExtensions.TileExtension.Percepts;
using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions_Test.TileExtension.Percepts
{
	[TestFixture]
	public class VisionTest
	{

		[Test]
		public void Vision_AgentWithSecondRingFilledByWalls7By7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing XmasEntity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * SSSSSSS
			 * SWWWWWS
			 * SWXXXWS
			 * SWXPXWS
			 * SWXXXWS
			 * SWWWWWS
			 * SSSSSSS
			 */

			TileMap map = new TileMap(new Size(3, 3));
			for (int i = -2; i <= 2; i++)
			{
				map[-2, i].AddEntity(new ImpassableWall());
				map[2, i].AddEntity(new ImpassableWall());
			}

			for (int i = -1; i <= 1; i++)
			{
				map[i, -2].AddEntity(new ImpassableWall());
				map[i, 2].AddEntity(new ImpassableWall());
			}

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 3, new Player());

			Dictionary<Point, Tile> expected = vision.AllTiles();
			List<Point> listedTiles = new List<Point>();

			for (int i = -2; i <= 2; i++)
				for (int j = -2; j <= 2; j++)
					listedTiles.Add(new Point(i, j));
			listedTiles.Remove(new Point(2, 2));
			listedTiles.Remove(new Point(-2, -2));
			listedTiles.Remove(new Point(-2, 2));
			listedTiles.Remove(new Point(2, -2));


			RemoveAllFromDictionary(expected,kv => !listedTiles.Contains(kv.Key));
			Dictionary<Point, Tile> actual = vision.VisibleTiles;
			List<KeyValuePair<Point, Tile>> act_diff = actual.Except(expected).ToList();
			List<KeyValuePair<Point, Tile>> exp_diff = expected.Except(actual).ToList();
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void Vision_AgentWithTwoOuterRingsFilledWithWalls7By7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing XmasEntity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * WWWWWWW
			 * WWWWWWW
			 * WWXXXWW
			 * WWXPXWW
			 * WWXXXWW
			 * WWWWWWW
			 * WWWWWWW
			 */

			TileMap map = new TileMap(new Size(3, 3));

			for (int x = -3; x <= 3; x++)
			{
				for (int y = -3; y <= 3; y++)
				{
					if (Math.Abs(x) > 1 || Math.Abs(y) > 1)
						map[x, y].AddEntity(new ImpassableWall());
				}
			}

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 3, new Player());

			Dictionary<Point, Tile> expected = vision.AllTiles();
			List<Point> listedTiles = new List<Point>();

			for (int i = -2; i <= 2; i++)
				for (int j = -2; j <= 2; j++)
					listedTiles.Add(new Point(i, j));
			listedTiles.Remove(new Point(2, 2));
			listedTiles.Remove(new Point(-2, -2));
			listedTiles.Remove(new Point(-2, 2));
			listedTiles.Remove(new Point(2, -2));


			RemoveAllFromDictionary(expected,kv => !listedTiles.Contains(kv.Key));
			Dictionary<Point, Tile> actual = vision.VisibleTiles;
			List<KeyValuePair<Point, Tile>> act_diff = actual.Except(expected).ToList();
			List<KeyValuePair<Point, Tile>> exp_diff = expected.Except(actual).ToList();
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void Vision_AgentWithWallToTheWest7by7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing XmasEntity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * XXXXXXX
			 * XXXXXXX
			 * XXXXXXX
			 * SSWPXXX
			 * XXXXXXX
			 * XXXXXXX
			 * XXXXXXX
			 */

			TileMap map = new TileMap(new Size(3, 3));
			map[-1, 0].AddEntity(new ImpassableWall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			Dictionary<Point, Tile> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, 0),
					new Point(-3, 0)
				};

			RemoveAllFromDictionary(expected,kv => unlistedTiles.Contains(kv.Key));
			Dictionary<Point, Tile> actual = vision.VisibleTiles;
			List<KeyValuePair<Point, Tile>> act_diff = actual.Except(expected).ToList();
			List<KeyValuePair<Point, Tile>> exp_diff = expected.Except(actual).ToList();
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void Vision_AgentWithWallsInCornersFiveTimesFiveGrid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing XmasEntity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * SXXXS
			 * XWXWX
			 * XXPXX
			 * XWXWX
			 * SXXXS
			 */

			TileMap map = new TileMap(new Size(2, 2));
			map[-1, -1].AddEntity(new ImpassableWall());
			map[1, -1].AddEntity(new ImpassableWall());
			map[-1, 1].AddEntity(new ImpassableWall());
			map[1, 1].AddEntity(new ImpassableWall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			Dictionary<Point, Tile> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, -2),
					new Point(2, -2),
					new Point(-2, 2),
					new Point(2, 2)
				};

			RemoveAllFromDictionary(expected,kv => unlistedTiles.Contains(kv.Key));
			Dictionary<Point, Tile> actual = vision.VisibleTiles;
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Vision_AgentWithWallsInNWCorner7by7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing XmasEntity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * SXXXXXX
			 * XSXXXXX
			 * XXWXXXX
			 * XXXPXXX
			 * XXXXXXX
			 * XXXXXXX
			 * XXXXXXX
			 */

			TileMap map = new TileMap(new Size(3, 3));
			map[-1, -1].AddEntity(new ImpassableWall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 3, new Player());

			Dictionary<Point, Tile> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, -2),
					new Point(-3, -3)
				};

			RemoveAllFromDictionary(expected,kv => unlistedTiles.Contains(kv.Key));
			Dictionary<Point, Tile> actual = vision.VisibleTiles;
			Assert.That(expected, Is.EquivalentTo(actual));
			
		}

		[Test]
		public void Vision_AgentWithWallsOnSidesFiveTimesFiveGrid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing XmasEntity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * XXSXX
			 * XXWXX
			 * SWPWS
			 * XXWXX
			 * XXSXX
			 */

			TileMap map = new TileMap(new Size(2, 2));
			map[-1, 0].AddEntity(new ImpassableWall());
			map[1, 0].AddEntity(new ImpassableWall());
			map[0, -1].AddEntity(new ImpassableWall());
			map[0, 1].AddEntity(new ImpassableWall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			Dictionary<Point, Tile> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, 0),
					new Point(2, 0),
					new Point(0, -2),
					new Point(0, 2)
				};

			RemoveAllFromDictionary(expected,kv => unlistedTiles.Contains(kv.Key));
			Dictionary<Point, Tile> actual = vision.VisibleTiles;
			Assert.AreEqual(expected, actual);
		}

		private void RemoveAllFromDictionary<TKey, TVal>(Dictionary<TKey, TVal> dic, Predicate<KeyValuePair<TKey, TVal>> selector) 
		{
			foreach(KeyValuePair<TKey,TVal> kv in dic.ToList())
			{
				if(selector(kv))
					dic.Remove(kv.Key);
			}
			//var temp = expected.Where(kv => !unlistedTiles.Contains(kv.Key));
			//expected = new Dictionary<Point, Tile>();
			//foreach (KeyValuePair<Point, Tile> kv in temp)
			//{
			//	expected.Add(kv.Key, kv.Value);
			//}
		}
	}
}