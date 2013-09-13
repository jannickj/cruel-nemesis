using JSLibrary.IiLang;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Actions;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Percepts;
using XmasEngineModel.Conversion;

namespace XmasEngineExtensions.EisExtension.Model
{
	public class EisConversionTool : XmasConversionTool<IilElement>
	{

		public EisConversionTool()
		{
			this.AddConverter(new GetAllPerceptsActionConverter());
			this.AddConverter(new EISPerceptCollectionSerializer());
			this.AddConverter(new EISSingleNumeralSerializer());
			AddConverter(new EISEmptyNamedPerceptSerializer());
		}
	}
}