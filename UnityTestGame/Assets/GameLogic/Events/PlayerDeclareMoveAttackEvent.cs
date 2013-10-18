using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;

namespace Assets.GameLogic.Events
{
	public class PlayerDeclareMoveAttackEvent : XmasEvent
	{
        private Player player;

        public Player Player
        {
            get { return player; }

        }


        public PlayerDeclareMoveAttackEvent(Player player)
        {
            this.player = player;
        }

        public Actions.MovePathAction MoveAction { get; set; }

        public Unit.UnitEntity Entity { get; set; }
    }
}
