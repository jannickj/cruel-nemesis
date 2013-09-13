using JSLibrary.Data;
using NUnit.Framework;
using XmasEngine_Test.ExampleObjects;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngine_Test.Model
{
	[TestFixture]
	public class GameWorldTest
	{
		[Test]
		public void GetEntityPosition_OneAgentInWorld_ReturnThatAgentPosition()
		{
			EventManager evtman = new EventManager();
			TileWorld world = new TileWorld(new Size(2, 2));
			world.EventManager = evtman;

			Agent agent = new Unit();
			world.AddEntity(agent,new TileSpawnInformation(new TilePosition(new Point(1, 2))));

			Point expected = new Point(1, 2);
			Point actual = ((TilePosition) world.GetEntityPosition(agent)).Point;
			Assert.AreEqual(expected, actual);
		}
	}
}