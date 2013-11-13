using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace Cruel.Map.Terrain
{
	public class TerrainEntity : XmasEntity
	{
        private TerrainTypes textype;

        public TerrainTypes TextureType
        {
            get { return textype; }
        }

        public TerrainEntity(TerrainTypes type)
        {
            this.textype = type;
        }


	}
}
