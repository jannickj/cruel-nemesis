using System;

namespace XmasEngineModel.Exceptions
{
	public class UnconvertableException : Exception
	{
		private object gobj;
	

		public UnconvertableException(object gobj) : this(gobj,null)
			
		{
		}

		public UnconvertableException(object gobj, Exception inner)
			: base("Conversion for object of type: " + gobj.GetType().Name + " Was not possible",inner)
		{
			this.gobj = gobj;
		}

		public object ConvertingObject
		{
			get { return gobj; }
		}
	}
}