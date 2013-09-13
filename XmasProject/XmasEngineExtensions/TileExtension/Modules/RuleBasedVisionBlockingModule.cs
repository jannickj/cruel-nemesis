using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Rule;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class RuleBasedVisionBlockingModule: VisionBlockingModule
	{
		private BlockingModule<XmasEntity> block = new BlockingModule<XmasEntity>();
		


		public override bool IsVisionBlocking(XmasEntity entity)
		{
			return block.IsBlocking(entity);
		}

		public void AddWillBlockRule<TDecider>(Predicate<XmasEntity> rule)
		{
			this.block.AddWillBlockRule<TDecider>(rule);
		}

		public void AddWillNotBLockRule<TDecider>(Predicate<XmasEntity> rule)
		{
			this.block.AddNotBlockRule<TDecider>(rule);
		}

		public void AddNewRuleLayer<TDecider>()
		{
			this.block.AddNewRuleLayer<TDecider>();
		}

	}
}