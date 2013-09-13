using NUnit.Framework;
using XmasEngine_Test.ExampleObjects;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineModel.EntityLib;
using System.Linq;
using System.Collections.Generic;

namespace XmasEngine_Test.Model.Entities.MapEntities
{
	[TestFixture]
	public class TileTest
	{
		[Test]
		public void CanContain_TerrainWithPowerUp_Returnstrue()
		{
			Unit a = new Unit();
			PowerUp p = new PowerUp();
			PowerUp p2 = new PowerUp();
			Tile t = new Tile();
			t.AddEntity(p);
			Assert.IsTrue(t.CanContain(a));
			Assert.IsTrue(t.CanContain(p2));
		}

		[Test]
		public void CanContain_aWall_ReturnsFalse()
		{
			Unit a = new Unit();
			Tile t = new Tile();
			ImpassableWall wall = new ImpassableWall();
			t.AddEntity(wall);
			Assert.IsFalse(t.CanContain(a));
		}

		[Test]
		public void CanContain_emptyTerrain_Returnstrue()
		{
			Unit a = new Unit();
			Tile t = new Tile();
			Assert.IsTrue(t.CanContain(a));
		}

		[Test]
		public void CanContain_terrainWithAnUnit_Returnsfalse()
		{
			Unit a = new Unit();
			Unit b = new Unit();
			PowerUp p = new PowerUp();
			Tile t = new Tile();
			t.AddEntity(a);
			Assert.IsFalse(t.CanContain(b));
			Assert.IsTrue(t.CanContain(p));
		}

		[Test]
		public void GetEntities_tileWithAnUnit_ReturnThatAgent()
		{
			Unit a = new Unit();
			Tile t = new Tile();
			t.AddEntity(a);

			Unit expected = a;
			Unit actual = t.Entities.OfType<Unit>().First();
			Assert.AreEqual(expected, actual);
		}
	}
}