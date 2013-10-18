using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
	public class PlayerAllowedToDeclareMoveAttackEvent : XmasEvent
	{
        public Player Player { get; set; }

        public bool Allowed { get; set; }
    }
}
