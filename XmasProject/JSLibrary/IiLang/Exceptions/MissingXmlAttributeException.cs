using System;

namespace JSLibrary.IiLang.Exceptions
{
	public class MissingXmlAttributeException : Exception
	{
		public MissingXmlAttributeException(string message)
			: base(message)
		{
		}
	}
}