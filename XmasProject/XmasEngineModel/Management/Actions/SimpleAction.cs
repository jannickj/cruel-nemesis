using System;

namespace XmasEngineModel.Management.Actions
{
	public class SimpleAction : EnvironmentAction
	{
		private Action<SimpleAction> action;

		public SimpleAction(Action<SimpleAction> action)
		{
			this.action = action;
		}

		protected override void Execute()
		{
			action(this);
			Complete();
		}
	}
}