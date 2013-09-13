using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Modules;

namespace XmasEngineExtensions.TileExtension.Entities
{
	public class ImpassableWall : XmasEntity
	{
		public ImpassableWall()
		{
			RegisterModule (new RuleBasedMovementBlockingModule());
			RuleBasedMovementBlockingModule movementBlocking = (RuleBasedMovementBlockingModule)this.Module<MovementBlockingModule>();
			movementBlocking.AddNewRuleLayer<ImpassableWall>();
			movementBlocking.AddWillBlockRule<ImpassableWall>(_ => true);

			this.RegisterModule(new RuleBasedVisionBlockingModule());
			var vmod = this.ModuleAs<VisionBlockingModule, RuleBasedVisionBlockingModule>();
			vmod.AddNewRuleLayer<ImpassableWall>();
			vmod.AddWillBlockRule<ImpassableWall>(_ => true);
		}
	}
}