using ConsoleXmasImplementation.Model.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang;
using XmasEngineExtensions.TileExtension.Actions;

namespace ConsoleXmasImplementation.Model.Conversion
{
	class EISGrabActionConverter : EISActionConverter<GrabPackageAction, EISGrabAction>
	{
		public override GrabPackageAction BeginConversionToXmas(EISGrabAction fobj)
		{
			return new GrabPackageAction ();
		}
	}
}
