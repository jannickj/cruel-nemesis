using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;

namespace Assets.UnityLogic.Game.Modules
{
	public class GraphicsModule : UniversalModule
	{
        public string TextureId { get; private set; }

        public GraphicsModule(string textureId)
        {
            this.TextureId = textureId;
        }
	}
}
