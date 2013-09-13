using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace XmasEngine_Test.ExampleObjects
{
	public class Wall : XmasEntity
	{
		public Wall()
		{
			this.RegisterModule(new RuleBasedMovementBlockingModule());

			var movmod = this.ModuleAs<MovementBlockingModule,RuleBasedMovementBlockingModule>();
			movmod.AddNewRuleLayer<Wall>();
			movmod.AddWillBlockRule<Wall>(_ => true);
		}

	}
}
