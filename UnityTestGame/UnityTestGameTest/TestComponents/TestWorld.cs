using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;

namespace UnityTestGameTest.TestComponents
{
    public class TestWorld : XmasWorld
    {
        public override ICollection<XmasEngineModel.EntityLib.XmasEntity> GetEntities(XmasEngineModel.World.XmasPosition pos)
        {
            throw new NotImplementedException();
        }

        public override XmasEngineModel.World.XmasPosition GetEntityPosition(XmasEngineModel.EntityLib.XmasEntity xmasEntity)
        {
            throw new NotImplementedException();
        }

        protected override bool OnAddEntity(XmasEngineModel.EntityLib.XmasEntity xmasEntity, XmasEngineModel.World.EntitySpawnInformation info)
        {
            throw new NotImplementedException();
        }

        protected override void OnRemoveEntity(XmasEngineModel.EntityLib.XmasEntity entity)
        {
            throw new NotImplementedException();
        }

        public override bool SetEntityPosition(XmasEngineModel.EntityLib.XmasEntity xmasEntity, XmasEngineModel.World.XmasPosition tilePosition)
        {
            throw new NotImplementedException();
        }
    }
}
