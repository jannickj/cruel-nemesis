using System;
using System.Xml;
using System.Xml.Serialization;
using JSLibrary.IiLang.Exceptions;

namespace JSLibrary.IiLang.Parameters
{
#pragma warning disable
	[XmlRoot("identifier")]
	public class IilIdentifier : IilParameter
	{
		public IilIdentifier(string value)
		{
			Value = value;
		}

		public IilIdentifier()
		{
		}

		public override string XmlTag
		{
			get { return "identifier"; }
		}

		public string Value { get; private set; }

		public override void WriteXml(XmlWriter writer)
		{
			if (String.IsNullOrEmpty(Value))
				throw new MissingXmlAttributeException("Error: Value not set.");

			writer.WriteAttributeString("value", Value);
		}

		public override void ReadXml(XmlReader reader)
		{
            reader.MoveToContent();
			Value = reader["value"];
            reader.Read();
            
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IilIdentifier id = (IilIdentifier) obj;
			return (Value == id.Value);
		}
	}
}