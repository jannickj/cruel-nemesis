using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Unit;
using XmasEngineExtensions.TileExtension;
using Assets.Library.PathFinding;

namespace Assets.GameLogic.PlayerCommands
{
	public class DeclareMoveAttackCommand : EntityXmasAction
	{



        public DeclareMoveAttackCommand(Path<TileWorld,TilePosition> movePath, UnitEntity Attack)
        {
        
        }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
