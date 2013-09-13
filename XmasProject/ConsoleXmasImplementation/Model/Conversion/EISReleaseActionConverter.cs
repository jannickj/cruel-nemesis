using ConsoleXmasImplementation.Model.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang;
using XmasEngineExtensions.TileExtension.Actions;

namespace ConsoleXmasImplementation.Model.Conversion
{
	class EISReleaseActionConverter : EISActionConverter<ReleasePackageAction, EISReleaseAction>
	{


        public override ReleasePackageAction BeginConversionToXmas(EISReleaseAction fobj)
        {
            return new ReleasePackageAction();
        }
    }
}
