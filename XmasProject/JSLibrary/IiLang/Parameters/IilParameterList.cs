using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace JSLibrary.IiLang.Parameters
{
#pragma warning disable
	[XmlRoot("parameterList")]
	public class IilParameterList : IilMultiParameter
	{
//		public List<Parameter> Parameters { get; private set; }

		public IilParameterList()
		{
		}

		public IilParameterList(params IilParameter[] ps)
			: base(ps)
		{
		}

		#region implemented abstract members of IILangElement

		public override void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			base.ReadXml(reader);
		}

		public override void WriteXml(XmlWriter writer)
		{
			base.WriteXml(writer);
		}

		#endregion

		public override string XmlTag
		{
			get { return "parameterList"; }
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
				return false;

			IilParameterList pl = (IilParameterList) obj;
			return (Parameters.SequenceEqual(pl.Parameters));
		}
	}
}