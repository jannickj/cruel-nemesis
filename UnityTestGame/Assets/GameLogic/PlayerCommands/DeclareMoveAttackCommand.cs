using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Unit;
using XmasEngineExtensions.TileExtension;
using Assets.Library.PathFinding;
using Assets.GameLogic.Events;
using Assets.GameLogic.Actions;
using Assets.GameLogic.Modules;

namespace Assets.GameLogic.PlayerCommands
{
	public class DeclareMoveAttackCommand : EntityXmasAction
	{

        private UnitEntity attackUnit;
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
            PlayerDeclareMoveAttackEvent evt = new PlayerDeclareMoveAttackEvent(player,(UnitEntity)this.Source,ma);
            this.EventManager.Raise(evt);
        }
    }
}
