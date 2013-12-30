using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Unit;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Cruel.GameLogic.Modules;
using Cruel.GameLogic.Events.UnitEvents;

namespace Cruel.GameLogic.Actions
{
    public class AttackUnitAction : EntityXmasAction
	{
        public UnitEntity Target { get; private set; }
        public int DamageOnResolve { get; private set; }

        public AttackUnitAction(UnitEntity target)
        {
            this.Target = target;
        }

        protected override void Execute()
        {

            TilePosition s = this.Source.PositionAs<TilePosition>();
            TilePosition e = Target.PositionAs<TilePosition>();
            AttackModule attmod = this.Source.Module<AttackModule>();

            Vector v = new Vector(s.Point, e.Point);
            float dist = v.Distance;
            float attackrange = (int)attmod.AttackRange;

            if (!attmod.CanReachPoint(s.Point, e.Point))
                return;

            int dmg = attmod.Damage;
            this.DamageOnResolve = dmg;
            int attackTime = attmod.AttackTime;

            this.Source.Raise(new UnitBeginAttackEvent(Source, Target, dmg, attackTime));

            Action attackfun =delegate
            {
                this.RunAction(this.Source, new DealDamageToUnitAction(Target, dmg));
            };

            if (attackTime > 0)
            {
                var timerAction = this.Factory.CreateTimer(attackfun);
                timerAction.SetSingle(attackTime);
                this.RunAction(timerAction);
            }
            else
                attackfun();
        }

        
    }
}
