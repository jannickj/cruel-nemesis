using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
	public class CardDiscardedEvent : XmasEvent
	{
        private Player player;
        public CardDiscardedEvent(Player player)
        {
            this.player = player;
        }
	}
}
