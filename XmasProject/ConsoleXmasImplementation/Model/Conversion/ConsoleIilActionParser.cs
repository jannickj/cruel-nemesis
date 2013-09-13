using ConsoleXmasImplementation.Model.Conversion;
using XmasEngineExtensions.EisExtension.Model;
using XmasEngineExtensions.TileEisExtension.ActionTypes;

namespace XmasEngineExtensions.TileEisExtension
{
	public class ConsoleIilActionParser : TileIilActionParser
	{
		public ConsoleIilActionParser()
		{
			this.Add<EISGrabAction>("grab");
            this.Add<EISReleaseAction>("release");
		}
	}
}
