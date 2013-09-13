using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Rule;

namespace XmasEngineExtensions.TileExtension.Modules
{
	internal class BlockingModule<TEntity> : RuleBasedModule<TEntity> where TEntity : XmasEntity
	{
		private Conclusion[] conclusions = new Conclusion[2];
		


		public BlockingModule()
		{
			conclusions[0] = new Conclusion("Non blocking");
			conclusions[1] = new Conclusion("Blocking");

			
		}

		public bool IsBlocking(TEntity ent)
		{
			Conclusion c = this.Conclude(ent);

			if (c == conclusions[0])
				return false;
			if (c == conclusions[1])
				return true;

			//If the object dont care it will not be blocking
			return false;
		}

		
		public void AddNewRuleLayer<TDecider>()
		{
			this.PushRuleLayer(typeof(TDecider));
		}

		public void AddNotBlockRule<TDecider>(Predicate<XmasEntity> rule)
		{
            Predicate<TEntity> pred = (ent => rule(ent));
			this.AddRule(typeof(TDecider), pred, conclusions[0]);
		}

		public void AddWillBlockRule<TDecider>(Predicate<XmasEntity> rule)
		{
            Predicate<TEntity> pred = (ent => rule(ent));
			this.AddRule(typeof(TDecider), pred, conclusions[1]);
		}

		public override Type ModuleType
		{
			get { throw new NotImplementedException(); }
		}
	}
}
