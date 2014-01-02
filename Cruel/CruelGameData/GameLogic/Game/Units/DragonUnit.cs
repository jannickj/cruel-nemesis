using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Modules;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic;

namespace CruelGameData.GameLogic.Game.Unit
{
    public class DragonUnit : UnitEntity
    {
        public DragonUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
            this.Module<AttackModule>().AttackRange = 1;
            this.Module<AttackModule>().Damage = 6;
            this.Module<HealthModule>().SetStartingHealth(6);
        }
    }
}
