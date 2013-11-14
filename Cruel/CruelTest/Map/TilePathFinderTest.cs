using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Cruel.GameLogic.Map;
using Cruel.Library.PathFinding;

namespace CruelTest.Map
{
    [TestClass]
    public class TilePathFinderTest : EngineTest
    {
        public TilePathFinderTest()
            : base(new TileWorldBuilder(new Size(2, 2)))
        {

        }

        [TestMethod]
        public void FindFirst_5x5EmptyMap_FindsPath()
        {
            TilePathFinder pathfinder = new TilePathFinder((TileWorld)this.World);

            var start = new TilePosition(new Point(0, 0));
            var goal = new TilePosition(new Point(0, -2));
            Path<TileWorld, TilePosition> foundPath;
            var hasPath = pathfinder.FindFirst(start, goal, out foundPath);

            Assert.IsTrue(hasPath);

        }
    }
}
