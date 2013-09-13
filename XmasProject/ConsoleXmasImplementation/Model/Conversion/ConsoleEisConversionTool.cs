using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileEisExtension;

namespace ConsoleXmasImplementation.Model.Conversion
{
	public class ConsoleEisConversionTool : TileEisConversionTool
	{
		public ConsoleEisConversionTool()
			: base()
		{
			this.AddConverter(new EISGrabActionConverter());
            this.AddConverter(new EISReleaseActionConverter());
		}
	}
}
