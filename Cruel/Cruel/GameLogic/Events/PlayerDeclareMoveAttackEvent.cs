using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.Unit;

namespace Cruel.GameLogic.Events
{
	public class PlayerDeclareMoveAttackEvent : XmasEvent
	{
        private Player player;
        private MovePathAction ma;
        private UnitEntity ent;
        private AttackUnitAction aa;
        private UnitEntity attackEnt;
        
        public AttackUnitAction AttackAction { get; private set; }

        public Player Player
        {
            get { return player; }

        }


        public PlayerDeclareMoveAttackEvent(Player player, UnitEntity ent, UnitEntity attackEnt, MovePathAction ma = null, AttackUnitAction aa = null)
        {
            this.player = player;
            this.ma = ma;
            this.ent = ent;
            this.aa = aa;
            this.attackEnt = attackEnt;
            this.AttackAction = aa;
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

        public UnitEntity AttackUnit 
        {
            get
            {
                return this.attackEnt;
            }
            
        }

    }
}
