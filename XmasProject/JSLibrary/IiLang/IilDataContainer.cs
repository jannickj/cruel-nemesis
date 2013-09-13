using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JSLibrary.IiLang.Exceptions;

namespace JSLibrary.IiLang
{
#pragma warning disable
	public abstract class IilDataContainer : IilElement
	{
		public IilDataContainer()
		{
			Parameters = new List<IilParameter>();
		}

		public IilDataContainer(String name, IilParameter[] ps)
		{
			Name = name;
			Parameters = new List<IilParameter>(ps);
		}

		public IilDataContainer(string name, LinkedList<IilParameter> ps)
		{
			Name = name;
			Parameters = ps.ToList();
		}

		public abstract string ChildXmlTag { get; }
		public string Name { get; private set; }
		public List<IilParameter> Parameters { get; private set; }


		public virtual void TransferFrom(IilDataContainer con)
		{
			Parameters = con.Parameters;
			Name = con.Name;
		}

		public void addParameter(IilParameter par)
		{
			Parameters.Add(par);
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IilDataContainer dc = (IilDataContainer) obj;
			return (Parameters.SequenceEqual(dc.Parameters) && Name.Equals(dc.Name));
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			if (reader.AttributeCount == 0)
				throw new MissingXmlAttributeException(@"Missing XML attribute ""value"".");
			Name = reader["name"];

			if (reader.IsEmptyElement)
			{
				reader.ReadEndElement();
				return;
			}


			if (reader.ReadToDescendant(ChildXmlTag))
			{
				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == ChildXmlTag)
				{
					reader.ReadStartElement();
					reader.MoveToContent();

					IilParameter p = IilParameter.fromString(reader.LocalName);
					p.ReadXml(reader);
					Parameters.Add(p);
					reader.ReadEndElement();
				}
			}
			
            
			reader.ReadEndElement();
			//reader.Read();
		}

		public override void WriteXml(XmlWriter writer)
		{
			if (String.IsNullOrEmpty(Name))
				throw new MissingXmlAttributeException(@"String ""Name"" must not be empty");

			writer.WriteAttributeString("name", Name);
			foreach (IilParameter p in Parameters)
			{
				writer.WriteStartElement(ChildXmlTag);
				writer.WriteStartElement(p.XmlTag);
				p.WriteXml(writer);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}