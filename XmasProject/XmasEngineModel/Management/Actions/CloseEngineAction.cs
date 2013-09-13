using XmasEngineModel.Management.Events;

namespace XmasEngineModel.Management.Actions
{
	public class CloseEngineAction : EnvironmentAction
	{
		protected override void Execute()
		{
			EventManager.Raise(new EngineCloseEvent());
			Complete();
		}
	}
}