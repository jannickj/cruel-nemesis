using System.Linq;
using JSLibrary.IiLang;
using JSLibrary.IiLang.DataContainers;
using XmasEngineModel;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Percepts
{
	public class EISPerceptCollectionSerializer : EISConverterToEIS<PerceptCollection, IilPerceptCollection>
	{
		public override IilPerceptCollection BeginConversionToForeign(PerceptCollection gobj)
		{
			return new IilPerceptCollection(gobj.Percepts.Select(p => (IilPercept) ConvertToForeign(p)).ToArray());
		}
	}
}