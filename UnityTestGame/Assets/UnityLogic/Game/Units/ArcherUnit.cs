using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;

namespace Assets.UnityLogic.Game.Units
{
	public class ArcherUnit : UnitEntity
    {
        public ArcherUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
            this.Module<AttackModule>().AttackRange = 5;
            this.Module<AttackModule>().Damage = 2;
            this.Module<HealthModule>().SetStartingHealth(2);
        }
	}
}
