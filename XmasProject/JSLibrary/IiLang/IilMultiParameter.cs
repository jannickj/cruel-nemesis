using System.Collections.Generic;
using System.Xml;

namespace JSLibrary.IiLang
{
	public abstract class IilMultiParameter : IilParameter
	{
		private List<IilParameter> parameters;


		public IilMultiParameter()
		{
			parameters = new List<IilParameter>();
		}

		public IilMultiParameter(IilParameter[] ps)
		{
			parameters = new List<IilParameter>(ps);
		}

		public List<IilParameter> Parameters
		{
			get { return parameters; }
		}

		public void AddParameter(IilParameter p)
		{
			parameters.Add(p);
		}

		public void AddParameter(IEnumerable<IilParameter> ps)
		{
			parameters.AddRange(ps);
		}

		#region IXmlSerializable implementation

		public override void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}

			reader.ReadStartElement();
			reader.MoveToContent();

			while (reader.MoveToContent() == XmlNodeType.Element)
			{
				IilParameter p = fromString(reader.LocalName);
				p.ReadXml(reader);
				parameters.Add(p);
			}
			reader.Read();
		}

		public override void WriteXml(XmlWriter writer)
		{
			foreach (IilElement p in parameters)
			{
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}