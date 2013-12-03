﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Events
{
    public class ManaRechargedEvent : XmasEvent
    {
        public Player player {get; set;}
        public ManaRechargedEvent(Player p)
        {
            player = p;
        }
    }
}
