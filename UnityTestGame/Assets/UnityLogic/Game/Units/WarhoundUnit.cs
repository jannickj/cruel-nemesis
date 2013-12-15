using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic;
using Cruel.GameLogic.Modules;

namespace Assets.UnityLogic.Game.Units
{
	public class WarhoundUnit : UnitEntity
    {
        public WarhoundUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
            this.Module<AttackModule>().AttackRange = 1;
            this.Module<AttackModule>().Damage = 3;
            this.Module<HealthModule>().SetStartingHealth(2);
        }
	}
}
