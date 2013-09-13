using XmasEngineExtensions.EisExtension.Model;
using XmasEngineExtensions.TileEisExtension.ActionTypes;

namespace XmasEngineExtensions.TileEisExtension
{
	public class TileIilActionParser : IILActionParser
	{
		public TileIilActionParser()
		{
			this.Add<EISMoveUnit>("move");
		}
	}
}
