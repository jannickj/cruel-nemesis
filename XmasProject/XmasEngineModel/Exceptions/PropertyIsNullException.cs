using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Exceptions
{
	public class PropertyIsNullException : Exception
	{
		private object obj;
		private string property;

		public string Property
		{
			get { return property; }
			set { property = value; }
		}

		public object Object
		{
			get { return obj; }
			
		}
		

		public PropertyIsNullException(string property, object obj)
			: base("The property: " + property + " is null, on " + obj)
		{
			this.property = property;
			this.obj = obj;
		}
	}
}
