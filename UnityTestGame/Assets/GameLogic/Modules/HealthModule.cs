using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic.Modules
{
	public class HealthModule : UniversalModule<XmasEntity>
	{
        public int Health { get; set; }
        public int VirtualHealth { get; set; }


	}
}
