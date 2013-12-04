using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Events
{
    public class PlayerGainedXPEvent : XmasEvent
    {
        private Player player;
        private int gainedxp;

        public int Gainedxp
        {
            get { return gainedxp; }
        }


        public Player Player
        {
            get { return player; }
        }
        public PlayerGainedXPEvent(Player player, int gainedxp)
        {
            // TODO: Complete member initialization
            this.player = player;
            this.gainedxp = gainedxp;
        }

    }
}
