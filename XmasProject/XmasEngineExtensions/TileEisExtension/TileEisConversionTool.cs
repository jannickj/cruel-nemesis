using XmasEngineExtensions.EisExtension.Model;
using XmasEngineExtensions.TileEisExtension.Conversion;

namespace XmasEngineExtensions.TileEisExtension
{
	public class TileEisConversionTool : EisConversionTool
	{

		public TileEisConversionTool()
		{
			this.AddConverter(new EisTileVisionSerializer());
			this.AddConverter(new MoveUnitActionConverter());
			this.AddConverter(new EisPositionSerializer());
		}
	}
}
