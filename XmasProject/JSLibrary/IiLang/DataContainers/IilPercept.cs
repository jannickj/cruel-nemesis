using System.Collections.Generic;
using System.Xml.Serialization;

namespace JSLibrary.IiLang.DataContainers
{
	[XmlRoot("percept")]
	public class IilPercept : IilDataContainer
	{
		public IilPercept()
		{
		}

		public IilPercept(string name, params IilParameter[] ps)
			: base(name, ps)
		{
		}

		public IilPercept(string name, LinkedList<IilParameter> ps)
			: base(name, ps)
		{
		}

		public override string XmlTag
		{
			get { return "percept"; }
		}

		public override string ChildXmlTag
		{
			get { return "perceptParameter"; }
		}
	}
}