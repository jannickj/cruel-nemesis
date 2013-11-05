using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events.UnitEvents
{
    class CardDrawnEvent : XmasEvent
	{
        private Player player;
        public CardDrawnEvent(Player player)
        {
            this.player = player;
        }
	}
}
