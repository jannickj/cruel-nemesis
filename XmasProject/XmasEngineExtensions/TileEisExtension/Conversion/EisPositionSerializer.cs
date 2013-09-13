using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.Percepts;

namespace XmasEngineExtensions.TileEisExtension.Conversion
{
	public class EisPositionSerializer : EISPerceptConverter<PositionPercept>
	{
		public override IilPercept BeginConversionToForeign(PositionPercept gobj)
		{
			TilePosition pos = (TilePosition) gobj.Position;

			return new IilPercept("position",new IilNumeral(pos.Point.X),new IilNumeral(pos.Point.Y));
		}
	}
}
