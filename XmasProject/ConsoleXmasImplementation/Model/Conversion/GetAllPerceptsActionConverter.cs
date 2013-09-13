using ConsoleXmasImplementation.Model.Actions;
using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang;
using XmasEngineExtensions.TileExtension.Actions;
using XmasEngineModel.Management.Actions;

namespace ConsoleXmasImplementation.Model.Conversion
{
	public class GrabActionConverter : EISActionConverter<GrabPackageAction, EISGrabAction>
	{
		public override GrabPackageAction BeginConversionToXmas(EISGrabAction fobj)
		{
			return new GrabPackageAction();
		}
	}
}