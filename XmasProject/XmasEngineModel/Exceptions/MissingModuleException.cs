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
		private XmasUniversal host;

		public Type ModuleType
		{
			get { return moduleType; }
		}

        public XmasUniversal Host
		{
			get { return host; }
			
		}

		public MissingModuleException(XmasUniversal host, Type moduleType)
			: base("Module Type " + moduleType.Name + " on type: " + host.GetType().Name + " is missing")
		{
			this.host = host;
			this.moduleType = moduleType;
		}
	}
}
