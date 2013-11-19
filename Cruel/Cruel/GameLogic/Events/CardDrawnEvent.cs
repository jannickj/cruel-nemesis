using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Events
{
    public class CardDrawnEvent : XmasEvent
	{
        public Player Player { get; private set; }
        public GameCard DrawnCard { get; private set; }
        public CardDrawnEvent(Player player, GameCard drawnCard)
        {
            this.Player = player;
            this.DrawnCard = drawnCard;
        }
	}
}
