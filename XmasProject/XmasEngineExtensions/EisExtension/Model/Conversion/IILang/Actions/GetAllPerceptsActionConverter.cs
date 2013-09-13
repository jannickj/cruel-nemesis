using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineModel.Management.Actions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Actions
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPerceptsAction, EISGetAllPercepts>
	{
		public override GetAllPerceptsAction BeginConversionToXmas(EISGetAllPercepts fobj)
		{
			return new GetAllPerceptsAction();
		}
	}
}