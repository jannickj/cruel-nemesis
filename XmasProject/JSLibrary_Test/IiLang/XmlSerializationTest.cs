using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using JSLibrary.IiLang;
using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using NUnit.Framework;

namespace JSLibrary_Test.IiLang
{
	[TestFixture]
	public class XmlSerializationTest
	{
		private static XmlWriter GenerateWriter()
		{
			StringBuilder sb = new StringBuilder();
			return XmlWriter.Create(sb);
		}

		[Test]
		public void PerceptCollectionWriter_XmlOfPerceptCollectionWithTwoPercepts_ReturnPerceptCollectionWithTwoChildren()
		{
			IilPerceptCollection actual_src = new IilPerceptCollection(new IilPercept("percept1", new IilNumeral(42)),
			                                                           new IilPercept("percept2", new IilIdentifier("id"))
				);

			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof (IilPerceptCollection));
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<perceptCollection>
					<percept name=""percept1"">
						<perceptParameter>
							<number value=""42"" />
						</perceptParameter>
					</percept>
					<percept name=""percept2"">
						<perceptParameter>
							<identifier value=""id"" />
						</perceptParameter>
					</percept>
				</perceptCollection>"
				);

			XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual(expected.ToString(), actual.ToString());
		}

		[Test]
		public void PerceptCollectionWriter_XmlOfPerceptCollectionWithNoPercepts_ReturnEmptyPerceptCollection()
		{
			IilPerceptCollection actual_src = new IilPerceptCollection();

			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof(IilPerceptCollection));
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<perceptCollection/>"
				);

			XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual(expected.ToString(), actual.ToString());
		}

		[Test]
		public void TryWriteXmlOfFunctionWithoutName_ThrowException()
		{
			IilFunction actual_src = new IilFunction();

			XmlSerializer serializer = new XmlSerializer(typeof (IilFunction));

			Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test]
		public void TryWriteXmlOfIdentifierWithoutValue_ThrowException()
		{
			IilIdentifier actual_src = new IilIdentifier();

			XmlSerializer serializer = new XmlSerializer(typeof (IilIdentifier));

			Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test]
		public void TryWriteXmlOfNumeralWithoutValue_ThrowException()
		{
			IilNumeral actual_src = new IilNumeral();

			XmlSerializer serializer = new XmlSerializer(typeof (IilNumeral));

			Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test]
		public void TryWriteXmlOfPerceptWithoutName_ThrowException()
		{
			IilPercept actual_src = new IilPercept();

			XmlSerializer serializer = new XmlSerializer(typeof (IilPercept));

			Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test]
		public void WriteXmlOfFunctionWithChildren_ReturnXmlWithContentAsChildren()
		{
			IilFunction actual_src = new IilFunction("test_fun", new IilNumeral(42));

			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof (IilFunction));
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<function name=""test_fun"">
					<number value=""42"" />
				</function>");
			XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual(expected.ToString(), actual.ToString());
		}

		[Test]
		public void WriteXmlOfIdentifierWithValue_XmlRepresentationOfIdentifierWithValue()
		{
			IilIdentifier actual_src = new IilIdentifier("test_id");
			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof (IilIdentifier));
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<identifier value=""test_id"" />");

			XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual(expected.ToString(), actual.ToString());
		}

		[Test]
		public void WriteXmlOfNumeralWithValue_XmlRepresentationOfNumeralWithValue()
		{
			IilNumeral actual_src = new IilNumeral(42);

			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(actual_src.GetType());
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<number value=""42"" />");

			string actualstr = XDocument.Parse(sb.ToString()).ToString();

			Assert.AreEqual(expected.ToString(), actualstr);
		}

		[Test]
		public void WriteXmlOfParameterListWithContent_ReturnXmlWithContentAsChildren()
		{
			IilParameterList actual_src = new IilParameterList(new IilIdentifier("test_id"), new IilNumeral(42));

			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof (IilParameterList));
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<parameterList>
					<identifier value=""test_id"" />
					<number value=""42"" />
				</parameterList>");

			XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual(expected.ToString(), actual.ToString());
		}

		[Test]
		public void WriteXmlOfPerceptWithChildren_ReturnXmlWithRepresentation()
		{
			IilPercept actual_src = new IilPercept("test_percept", new IilNumeral(42), new IilIdentifier("test_id"));

			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof (IilPercept));
			serializer.Serialize(XmlWriter.Create(sb), actual_src);

			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<percept name=""test_percept"">
					<perceptParameter>
						<number value=""42"" />
					</perceptParameter>
					<perceptParameter>
						<identifier value=""test_id"" />
					</perceptParameter>
				</percept>");

			XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual(expected.ToString(), actual.ToString());
		}
	}
}