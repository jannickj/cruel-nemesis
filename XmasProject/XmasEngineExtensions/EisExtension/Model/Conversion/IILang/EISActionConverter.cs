using XmasEngineModel.Conversion;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang
{
	public abstract class EISActionConverter<ActionType, EISActionType> : XmasConverterToXmas<ActionType, EISActionType>
		where ActionType : EntityXmasAction
		where EISActionType : EISAction
	{
		
	}
}