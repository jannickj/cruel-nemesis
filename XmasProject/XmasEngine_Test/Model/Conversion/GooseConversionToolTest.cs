using NUnit.Framework;
using XmasEngineModel;
using XmasEngineModel.Conversion;
using XmasEngineModel.Exceptions;

namespace XmasEngine_Test.Model.Conversion
{
	[TestFixture]
	public class XmasConversionToolTest
	{
		private class XmasA : XmasObject
		{
		}

		private class XmasB : XmasA
		{
		}

		private class ForeignA
		{
		}

		private class ForeignB : ForeignA
		{
		}

		private class MockAToAConvert : XmasConverter<XmasA, ForeignA>
		{
			public override XmasA BeginConversionToXmas(ForeignA fobj)
			{
				return new XmasA();
			}

			public override ForeignA BeginConversionToForeign(XmasA gobj)
			{
				return new ForeignA();
			}
		}

		private class MockBToBConvert : XmasConverter<XmasB, ForeignB>
		{
			public override XmasB BeginConversionToXmas(ForeignB fobj)
			{
				return new XmasB();
			}

			public override ForeignB BeginConversionToForeign(XmasB gobj)
			{
				return new ForeignB();
			}
		}

		[Test]
		public void Convert_ForeignBToXmasBWithConverter_ShouldReturnXmasB()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockBToBConvert());

			Assert.IsInstanceOf<XmasB>(ctool.ConvertToXmas(new ForeignB()));
		}

		[Test]
		public void Convert_ForeignBToXmasBWithoutConverter_ShouldNOTUseXmasAToForeignAConverterInstead()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockAToAConvert());

			Assert.Throws<UnconvertableException>(() => ctool.ConvertToXmas(new ForeignB()));
		}

		[Test]
		public void Convert_XmasAToForeignAWithConverter_GoesFromXmasAToForeignA()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockAToAConvert());

			Assert.IsInstanceOf<ForeignA>(ctool.ConvertToForeign(new XmasA()));
		}


		[Test]
		public void Convert_XmasBToForeignBWithoutConverter_ShouldUseXmasAToForeignAConverterInstead()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockAToAConvert());

			Assert.IsInstanceOf<ForeignA>(ctool.ConvertToForeign(new XmasB()));
		}
	}
}