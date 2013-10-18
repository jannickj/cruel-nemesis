using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Actions;
using Assets.GameLogic.Unit;

namespace Assets.GameLogic.Events
{
	public class PlayerDeclareMoveAttackEvent : XmasEvent
	{
        private Player player;
        private MovePathAction ma;
        private UnitEntity ent;

        public Player Player
        {
            get { return player; }

        }


        public PlayerDeclareMoveAttackEvent(Player player, UnitEntity ent, MovePathAction ma = null)
        {
            this.player = player;
            this.ma = ma;
            this.ent = ent;
        }

        public MovePathAction MoveAction 
        {
            get
            {
                return ma;
            }
        }

        public UnitEntity Entity 
        {
            get
            {
                return ent;
            }
        }
    }
}
