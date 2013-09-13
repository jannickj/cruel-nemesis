using XmasEngineModel.EntityLib;
using XmasEngineModel.Management.Events;
using XmasEngineModel.World;

namespace XmasEngineModel.Management.Actions
{
	public class AddEntityAction : EnvironmentAction
	{
		private XmasEntity ent;
		private EntitySpawnInformation info;

		public AddEntityAction(XmasEntity ent, EntitySpawnInformation info)
		{
			this.ent = ent;
			this.info = info;
		}

		protected override void Execute()
		{
			ent.ActionManager = ActionManager;
			ent.EventManager = EventManager;
			ent.World = World;
			ent.Factory = Factory;

			if (World.AddEntity (ent, info)) {
				this.Complete ();
			} else {
				this.Fail ();
			}

		}
	}
}