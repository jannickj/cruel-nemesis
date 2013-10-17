using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public abstract class VisionBlockingModule : UniversalModule
	{

		public abstract bool IsVisionBlocking(XmasEntity entity);

		public override Type ModuleType
		{
			get { return typeof(VisionBlockingModule); }
		}
	}
}
