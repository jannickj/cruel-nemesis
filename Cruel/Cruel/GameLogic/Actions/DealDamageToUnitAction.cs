using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;
using Cruel.GameLogic.Events.UnitEvents;

namespace Cruel.GameLogic.Actions
{
    public class DealDamageToUnitAction : EntityXmasAction
	{
        public UnitEntity Target { get; private set; }
        public int Damage { get; private set; }

        public DealDamageToUnitAction(UnitEntity target, int dmg)
        {
            this.Target = target;
            this.Damage = dmg;
        }

        protected override void Execute()
        {
            HealthModule hmod = this.Target.Module<HealthModule>();
            
            hmod.IncreaseCurrentHealth(-Damage);
            this.Source.Raise(new UnitDealsDamageEvent(Source, Target, Damage));
            var dmgtaker = Target;
            var dmgdealer = (UnitEntity)Source;
            this.Target.Raise(new UnitTakesDamageEvent(dmgtaker, dmgdealer, Damage));
        }
    }
}
