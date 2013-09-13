using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Exceptions
{
	public class MissingModuleException : Exception
	{
		private Type moduleType;
		private XmasEntity entity;

		public Type ModuleType
		{
			get { return moduleType; }
		}

		public XmasEntity Entity
		{
			get { return entity; }
			
		}

		public MissingModuleException(XmasEntity entity, Type moduleType)
			: base("Module Type " + moduleType.Name + " on entity type: " + entity.GetType().Name + " is missing")
		{
			this.entity = entity;
			this.moduleType = moduleType;
		}
	}
}
