using JSLibrary.IiLang;
using XmasEngineModel;
using XmasEngineModel.Conversion;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang
{
	public abstract class EISConverterToEIS<XmasType, EISType> : XmasConverterToForeign<XmasType, EISType>
		where EISType : IilElement
		where XmasType : XmasObject
	{
	}
}