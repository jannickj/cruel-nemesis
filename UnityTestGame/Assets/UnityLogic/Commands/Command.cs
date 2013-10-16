using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using Assets.UnityLogic.Gui;

namespace Assets.UnityLogic.Commands
{
	public abstract class Command : XmasActor
	{

        public GuiController GuiController { get; set; }
        public abstract void Update();

        public bool Finished { get; protected set; }
    }
}
