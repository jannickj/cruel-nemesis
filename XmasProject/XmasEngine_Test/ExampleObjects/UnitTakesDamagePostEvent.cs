using XmasEngine_Test.ExampleObjects;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Events
{
	public class UnitTakesDamagePostEvent : XmasEvent
	{
		private int actualDmg;
		private int dmg;
		private Unit source;
		private Unit target;

		public UnitTakesDamagePostEvent(Unit source, Unit target, int dmg, int actualDmg)
		{
			this.source = source;
			this.target = target;
			this.dmg = dmg;
			this.actualDmg = actualDmg;
		}


		public int Damage
		{
			get { return dmg; }
		}


		public int ActualDmg
		{
			get { return actualDmg; }
		}

		public Unit Source
		{
			get { return source; }
		}

		public Unit Target
		{
			get { return target; }
		}
	}
}