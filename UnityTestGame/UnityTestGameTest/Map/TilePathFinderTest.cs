using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Assets.GameLogic.Map;

namespace UnityTestGameTest.Map
{
    [TestClass]
    public class TilePathFinderTest : EngineTest
    {
        public TilePathFinderTest()
            : base(new TileWorld(new Size(2, 2)))
        {

        }

        [TestMethod]
        public void FindFirst_5x5EmptyMap_FindsPath()
        {
            TilePathFinder pathfinder = new TilePathFinder((TileWorld)this.World);

            var start = new TilePosition(new Point(0, 0));
            var goal = new TilePosition(new Point(0, -2));
            bool foundPath;
            var path = pathfinder.FindFirst(start, goal, out foundPath);

            Assert.IsTrue(foundPath);

        }
    }
}
