using XmasEngineModel.EntityLib;
using XmasEngineModel.Management.Events;
using XmasEngineModel.World;

namespace XmasEngineModel.Management.Actions
{
	public class RemoveEntityAction : EnvironmentAction
	{
		private XmasEntity ent;
		
		public RemoveEntityAction(XmasEntity ent)
		{
			this.ent = ent;
		}
		
		protected override void Execute()
		{
			World.RemoveEntity(ent);
			Complete ();
			
		}
	}
}

