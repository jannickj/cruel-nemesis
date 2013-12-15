using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Modules;

namespace Cruel.GameLogic.Unit
{
    public class HeroUnit : UnitEntity
    {
        public HeroUnit(Player owner) : base(owner)
        {
            owner.Hero = this;
            var hpmod = this.Module<HealthModule>();
            hpmod.SetStartingHealth(25);
        }
    }
}
