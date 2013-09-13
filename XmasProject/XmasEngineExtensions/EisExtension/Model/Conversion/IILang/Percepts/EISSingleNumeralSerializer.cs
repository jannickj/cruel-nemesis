using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel.Percepts;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Percepts
{
	public class EISSingleNumeralSerializer : EISPerceptConverter<SingleNumeralPercept>
	{
		public override IilPercept BeginConversionToForeign(SingleNumeralPercept gobj)
		{
			return new IilPercept(gobj.Name, new IilNumeral(gobj.Value));
		}
	}
}