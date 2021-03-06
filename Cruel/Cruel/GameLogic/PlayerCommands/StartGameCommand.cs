﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;

namespace Cruel.GameLogic.PlayerCommands
{
	public class StartGameCommand : EnvironmentAction
	{

        protected override void Execute()
        {
            this.EventManager.Raise(new GamePreStartEvent());
            this.EventManager.Raise(new GameStartEvent());
        }
    }
}
