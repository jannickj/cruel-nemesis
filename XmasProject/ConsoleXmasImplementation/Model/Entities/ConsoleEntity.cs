using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model.Entities
{
	public abstract class ConsoleEntity : XmasEntity
	{
	

		public ConsoleEntity()
		{
			this.RegisterModule(new RuleBasedMovementBlockingModule());
			RuleBasedMovementBlockingModule blockingModule = (RuleBasedMovementBlockingModule)this.Module<MovementBlockingModule>();
			blockingModule.AddNewRuleLayer<ConsoleEntity>();
			this.RegisterModule(new RuleBasedVisionBlockingModule());

			var mod = this.ModuleAs<VisionBlockingModule, RuleBasedVisionBlockingModule>();

			mod.AddNewRuleLayer<ConsoleEntity>();
			mod.AddWillBlockRule<ConsoleEntity>(_ => true);
		}

	}
}
