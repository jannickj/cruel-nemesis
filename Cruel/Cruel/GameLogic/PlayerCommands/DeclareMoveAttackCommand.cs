using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Unit;
using XmasEngineExtensions.TileExtension;
using Cruel.Library.PathFinding;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.Modules;

namespace Cruel.GameLogic.PlayerCommands
{
	public class DeclareMoveAttackCommand : EntityXmasAction
	{

        private UnitEntity attackUnit = null;
        private Path<TileWorld, TilePosition> path;
        private Player player;

        public DeclareMoveAttackCommand(Player player, Path<TileWorld, TilePosition> movePath)
        {
            this.player = player;
            this.path = movePath;
        }

        public DeclareMoveAttackCommand(Player player, Path<TileWorld, TilePosition> movePath, UnitEntity Attack) : this(player, movePath)
        {
            this.attackUnit = Attack;
        }

        

        protected override void Execute()
        {
            MovePathAction ma = new MovePathAction(this.Source, path, this.Source.Module<MoveModule>().MoveDuration);
            AttackUnitAction aa = new AttackUnitAction(this.attackUnit);
            PlayerDeclareMoveAttackEvent evt = new PlayerDeclareMoveAttackEvent(player,(UnitEntity)this.Source,this.attackUnit,ma,aa);
            this.EventManager.Raise(evt);
        }
    }
}
