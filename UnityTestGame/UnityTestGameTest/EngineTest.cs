using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using UnityTestGameTest.TestComponents;

namespace UnityTestGameTest
{
    public class EngineTest : XmasActor
    {
        public XmasModel Engine { get; private set; }

        public EngineTest()
        {
            this.EventManager = new EventManager();
            this.ActionManager = new ActionManager(this.EventManager);
            this.Factory = new XmasFactory(this.ActionManager);
            this.World = new MockWorld();
            this.Engine = new XmasModel(this.World, this.ActionManager, this.EventManager, this.Factory);
        }
    }
}
