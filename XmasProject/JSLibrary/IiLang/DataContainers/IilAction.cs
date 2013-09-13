using System.Collections.Generic;
using System.Xml.Serialization;

namespace JSLibrary.IiLang.DataContainers
{
	[XmlRoot("action")]
	public class IilAction : IilDataContainer
	{
		public IilAction()
		{
		}

		public IilAction(string name, params IilParameter[] ps)
			: base(name, ps)
		{
		}

		public IilAction(string name, LinkedList<IilParameter> ps)
			: base(name, ps)
		{
		}

		public override string XmlTag
		{
			get { return "action"; }
		}

		public override string ChildXmlTag
		{
			get { return "actionParameter"; }
		}
	}
}