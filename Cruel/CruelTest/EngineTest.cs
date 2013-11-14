using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using CruelTest.TestComponents;
using XmasEngineExtensions.TileExtension;

namespace CruelTest
{
    public class EngineTest : XmasActor
    {
        public XmasModel Engine { get; private set; }

        public EngineTest()
            : this(new TileWorldBuilder(new JSLibrary.Data.Size(1, 1)))
        {
            
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
