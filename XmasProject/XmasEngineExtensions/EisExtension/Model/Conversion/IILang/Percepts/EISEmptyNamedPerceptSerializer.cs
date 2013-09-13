using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel.Percepts;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Percepts
{
	public class EISEmptyNamedPerceptSerializer : EISPerceptConverter<EmptyNamedPercept>
	{
		public override IilPercept BeginConversionToForeign(EmptyNamedPercept gobj)
		{
			return new IilPercept(gobj.Name);
		}
	}
}