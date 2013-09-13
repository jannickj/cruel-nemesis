using System;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Exceptions
{
	public class EntityException : Exception
	{
		private XmasEntity xmasEntity;

		public EntityException(XmasEntity xmasEntity)
		{
			this.xmasEntity = xmasEntity;
		}

		public EntityException(XmasEntity e, string msg) : base(msg)
		{
			xmasEntity = e;
		}

		public XmasEntity XmasEntity
		{
			get { return xmasEntity; }
		}
	}
}