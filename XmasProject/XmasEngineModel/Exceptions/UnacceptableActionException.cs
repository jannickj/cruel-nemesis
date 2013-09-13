using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineModel.Exceptions
{
	public class UnacceptableActionException : Exception
	{
		private XmasAction action;
		private XmasEntity xmasEntity;

		public UnacceptableActionException(XmasAction action, XmasEntity xmasEntity)
			: base("XmasEntity: [" + xmasEntity.GetType().Name + "] can't accept action: [" + action.GetType().Name + "]")
		{
			this.action = action;
			this.xmasEntity = xmasEntity;
		}

		public XmasAction Action
		{
			get { return action; }
		}

		public XmasEntity XmasEntity
		{
			get { return xmasEntity; }
		}
	}
}