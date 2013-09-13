using System;
using XmasEngineModel.Conversion;

namespace XmasEngineModel.Exceptions
{
	public class UnableToConvertException : Exception
	{
		private XmasConverter converter;

		public UnableToConvertException(XmasConverter converter)
			: base("Converter: " + converter.GetType().Name + "does not support the conversion")
		{
			this.converter = converter;
		}

		public XmasConverter Converter
		{
			get { return converter; }
		}
	}
}