using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace JSLibrary.IiLang
{
	public abstract class IilElement : IXmlSerializable
	{
		public abstract string XmlTag { get; }

		#region IXmlSerializable implementation

		public XmlSchema GetSchema()
		{
			return null;
		}

		public abstract void ReadXml(XmlReader reader);

		public abstract void WriteXml(XmlWriter writer);

		#endregion
	}
}