using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using JSLibrary.IiLang.DataContainers;

namespace JSLibrary.IiLang
{
	[XmlRoot("perceptCollection")]
	public class IilPerceptCollection : IilElement, IXmlSerializable
	{
		private List<IilPercept> percepts = new List<IilPercept>();

		public IilPerceptCollection()
		{
		}

		public IilPerceptCollection(params IilPercept[] ps)
		{
			foreach (IilPercept p in ps)
				percepts.Add(p);
		}

		public List<IilPercept> Percepts
		{
			get { return percepts; }
		}

//        #region IXmlSerializable implementation
//        public System.Xml.Schema.XmlSchema GetSchema ()
//        {
//            return null;
//        }

//        public void ReadXml (System.Xml.XmlReader reader)
//        {
//            // No unit tests, we are only interested in writing perceptCollections
//            throw new NotImplementedException ();
////			reader.MoveToContent ();
////			
////			if (reader.IsEmptyElement) {
////				reader.Read ();
////			}
////			
////			if (reader.ReadToDescendant ("percept")) {
////				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "percept") {
////					
////					reader.ReadStartElement();
////					reader.MoveToContent();
////					
////					IILPercept p = new IILPercept();
////					p.ReadXml(reader);
////					Percepts.Add(p);
////					reader.Read();
////				}
////			}
////			reader.Read();		
//        }

//        public void WriteXml (System.Xml.XmlWriter writer)
//        {
//            foreach (IILPercept p in percepts) {
//                writer.WriteStartElement("percept");
//                p.WriteXml(writer);
//                writer.WriteEndElement();
//            }
//        }
//        #endregion

		public override string XmlTag
		{
			get { return "perceptCollection"; }
		}

		public override void ReadXml(XmlReader reader)
		{
			// No unit tests, we are only interested in writing perceptCollections
			throw new NotImplementedException();
			//			reader.MoveToContent ();
			//			
			//			if (reader.IsEmptyElement) {
			//				reader.Read ();
			//			}
			//			
			//			if (reader.ReadToDescendant ("percept")) {
			//				while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "percept") {
			//					
			//					reader.ReadStartElement();
			//					reader.MoveToContent();
			//					
			//					IILPercept p = new IILPercept();
			//					p.ReadXml(reader);
			//					Percepts.Add(p);
			//					reader.Read();
			//				}
			//			}
			//			reader.Read();	
		}

		public override void WriteXml(XmlWriter writer)
		{
			
			foreach (IilPercept p in percepts)
			{
				writer.WriteStartElement("percept");
				p.WriteXml(writer);
				writer.WriteEndElement();
			}
		}
	}
}