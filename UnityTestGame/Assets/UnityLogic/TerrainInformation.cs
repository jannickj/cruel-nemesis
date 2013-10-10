using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using Assets.Map.Terrain;
using UnityEngine;

namespace Assets.UnityLogic
{
    public class TerrainInformation : MonoBehaviour
	{
        private TerrainEntity entity;

        public TerrainEntity Terrain
        {
            get { return entity; }
        }

        public void SetTerrain(TerrainEntity entity)
        {
            this.entity = entity;
        }
	}
}
