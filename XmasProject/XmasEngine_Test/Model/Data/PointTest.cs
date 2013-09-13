using JSLibrary.Data;
using NUnit.Framework;

namespace XmasEngine_Test.Model.Data
{
	[TestFixture]
	public class PointTest
	{
		[Test]
		public void PointAddition_AddPointToPoint_NewPoint()
		{
			Point actual = new Point(1, 1) + new Point(1, 1);
			Point expected = new Point(2, 2);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void PointSubtraction_SubtractPointFromPoint_NewPoint()
		{
			Point actual = new Point(1, 1) - new Point(1, 1);
			Point expected = new Point(0, 0);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void PointVectorAddition_AddVectorToPoint_NewPoint()
		{
			Point actual = new Point(1, 1) + new Vector(1, 1);
			Point expected = new Point(2, 2);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void PointVectorSubtraction_SubtractPointFromVector_NewPoint()
		{
			Point actual = new Point(1, 1) - new Vector(1, 1);
			Point expected = new Point(0, 0);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void VectorPointAddition_AddPointToVector_NewPoint()
		{
			Point actual = new Vector(1, 1) + new Point(1, 1);
			Point expected = new Point(2, 2);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void VectorPointSubtraction_SubtractVectorFromPoint_NewPoint()
		{
			Point actual = new Point(1, 1) - new Vector(1, 1);
			Point expected = new Point(0, 0);
			Assert.AreEqual(expected, actual);
		}
	}
}