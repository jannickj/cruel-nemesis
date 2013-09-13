using System;

namespace JSLibrary.Data.GenericEvents
{
	public delegate void UnaryValueHandler<T>(object sender, UnaryValueEvent<T> evt);

	public class UnaryValueEvent<T> : EventArgs
	{
		private T val;

		public UnaryValueEvent(T val)
		{
			this.val = val;
		}

		public T Value
		{
			get { return val; }
		}
	}
}