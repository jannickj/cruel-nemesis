using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityTestGameTest.TestComponents;
using JSLibrary.Data;
using Cruel.Library.PathFinding;

namespace UnityTestGameTest.Library.PathFinding
{
    [TestClass]
    public class PathFinderTest
    {
        [TestMethod]
        public void FindFirst_Map3x3OnePath_FindsCorrectPath()
        {
            
            int[,] map = new int[3, 3];

            map[0, 1] = 1;
            map[1, 1] = 1;

            MockPathFinder pathfinder = new MockPathFinder(map);

            Path<int[,], Point> foundPath;
            bool hasPath = pathfinder.FindFirst(new Point(0, 0), new Point(0, 2),out foundPath);

            Assert.IsTrue(hasPath);
        }
    }
}
