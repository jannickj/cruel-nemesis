using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using JSLibrary.IiLang.Exceptions;

namespace JSLibrary.IiLang.Parameters
{
#pragma warning disable
	[XmlRoot("number")]
	public class IilNumeral : IilParameter
	{
		public IilNumeral(Double value)
		{
			Value = value;
		}

		public IilNumeral()
		{
			Value = Double.NaN;
		}

		public override string XmlTag
		{
			get { return "number"; }
		}

		public double Value { get; private set; }

		public override void WriteXml(XmlWriter writer)
		{
			if (Double.IsNaN(Value))
				throw new MissingXmlAttributeException("Error: Value not set.");

			writer.WriteAttributeString("value", Value.ToString());
		}

		public override void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException(@"Missing XML attribute ""value"".");
			Value = Convert.ToDouble(reader["value"], CultureInfo.InvariantCulture);
			reader.Read();
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IilNumeral num = (IilNumeral) obj;
			return (Value == num.Value);
		}
	}
}