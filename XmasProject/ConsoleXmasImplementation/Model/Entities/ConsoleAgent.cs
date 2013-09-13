using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model.Entities
{
	public abstract class ConsoleAgent : Agent
	{
		public ConsoleAgent(string name) : base(name)
		{
			this.RegisterModule(ConstructSpeedModule());
			this.RegisterModule(new RuleBasedMovementBlockingModule());

			RuleBasedMovementBlockingModule blockingModule = (RuleBasedMovementBlockingModule)this.Module<MovementBlockingModule>();
			blockingModule.AddNewRuleLayer<ConsoleAgent>();
			blockingModule.AddWillBlockRule<ConsoleAgent>(entity => entity is Agent);

			this.RegisterModule(new RuleBasedVisionBlockingModule());

			var mod = this.ModuleAs<VisionBlockingModule, RuleBasedVisionBlockingModule>();

			mod.AddNewRuleLayer<ConsoleAgent>();
			mod.AddWillNotBLockRule<ConsoleAgent>(_ => true);
		}

		

		protected abstract SpeedModule ConstructSpeedModule();

	}
}
