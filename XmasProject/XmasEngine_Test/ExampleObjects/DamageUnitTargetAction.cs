using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace XmasEngine_Test.ExampleObjects
{
	public class DamageUnitTargetAction : EntityXmasAction<Unit>
	{
		private int dmg;
		private Unit target;

		public DamageUnitTargetAction(Unit target, int dmg)
		{
			this.target = target;
			this.dmg = dmg;
		}


		protected override void Execute()
		{
			UnitTakesDamagePreEvent pre = new UnitTakesDamagePreEvent(Source, target, dmg);
			target.Raise(pre);
			int actualDamage = pre.ActualDmg;
			int newhp = target.Health - actualDamage;
			Source.Health = newhp < 0 ? 0 : newhp;
			UnitTakesDamagePostEvent post = new UnitTakesDamagePostEvent(Source, target, dmg, actualDamage);
			target.Raise(post);
			Complete();
		}
	}
}