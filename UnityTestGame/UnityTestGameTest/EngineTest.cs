using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using UnityTestGameTest.TestComponents;
using XmasEngineExtensions.TileExtension;

namespace UnityTestGameTest
{
    public class EngineTest : XmasActor
    {
        public XmasModel Engine { get; private set; }

        public EngineTest() : this(new MockWorld())
        {
            
        }


        public EngineTest(XmasWorld world)
        {
            var wb = new TileWorldBuilder(new JSLibrary.Data.Size(1, 1));
            this.EventManager = new EventManager();
            this.ActionManager = new ActionManager(this.EventManager);
            this.Factory = new XmasFactory(this.ActionManager);
            this.Engine = new XmasModel(wb, this.ActionManager, this.EventManager, this.Factory);
            this.World = this.Engine.World;
        }
    }
}
