using JSLibrary.Data;
using NUnit.Framework;
using XmasEngine_Test.ExampleObjects;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineModel.EntityLib;
using System.Linq;

namespace XmasEngine_Test.Model
{
	[TestFixture]
	public class GameMapTest
	{
	

		[Test]
		public void MisplacedWallChunk_RequestBuildWallOnUnit_ReturnWallNotBuilt()
		{
			TileMap map = new TileMap(new Size(0, 0));

			map[0, 0].AddEntity(new Player());
			//map.AddChunk<Wall> (new Point (0, 0), new Point (0, 0));

			Assert.That(map[0, 0].Entities, Has.Some.InstanceOf<Player>());
			Assert.That(map[0, 0].Entities, Has.None.InstanceOf<ImpassableWall>());
		}

		[Test]
		public void OutOfBoundsTile_GetTileOutSideMap_ReturnsTileWithImpassableWall()
		{
			TileMap map = new TileMap(new Size(0, 0));
			XmasEntity actual = map[0, 1].Entities.First();

			Assert.IsInstanceOf<ImpassableWall>(actual);
		}

		[Test]
		public void OutOfBoundsWallChunk_RequestBuildWallOutsideMap_ReturnWallNotBuilt()
		{
			TileMap map = new TileMap(new Size(0, 0));
			//map.AddChunk<Wall> (new Point (1, 1), new Point (1, 1));
			XmasEntity actual = map[1, 1].Entities.First();

			Assert.IsInstanceOf<ImpassableWall>(actual);
		}
	}
}