using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using CruelTest.TestComponents;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.Management.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CruelTest
{
    public class EngineTest : XmasActor
    {
        public XmasModel Engine { get; private set; }

        

        public EngineTest()
            : this(new TileWorldBuilder(new JSLibrary.Data.Size(1, 1)))
        {
            
        }

        public void FailTestOnEngineCrash()
        {
            this.Engine.EventManager.Register(new Trigger<ActionFailedEvent>(evt => Assert.Fail(evt.Exception.Message)));
            this.Engine.EventManager.Register(new Trigger<TriggerFailedEvent>(evt => Assert.Fail(evt.Exception.Message)));
        }


        public EngineTest(TileWorldBuilder wb)
        {
            this.EventManager = new EventManager();
            this.ActionManager = new ActionManager(this.EventManager);
            this.Factory = new XmasFactory(this.ActionManager);
            this.Engine = new XmasModel(wb, this.ActionManager, this.EventManager, this.Factory);
            this.World = this.Engine.World;
        }
    }
}
