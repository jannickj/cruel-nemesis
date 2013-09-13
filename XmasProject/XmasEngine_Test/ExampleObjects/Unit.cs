using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace XmasEngine_Test.ExampleObjects
{
	public class Unit : Agent
	{
		public Unit()
			: this("irrelevant_name")
		{

		}

		public Unit(string name)
			: base(name)
		{
			this.Health = 0;
			this.RegisterModule(new RuleBasedMovementBlockingModule());
			var movmod = this.ModuleAs<MovementBlockingModule, RuleBasedMovementBlockingModule>();
			movmod.AddNewRuleLayer<Unit>();
			movmod.AddWillBlockRule<Unit>(ent => ent is Unit);
		}

		public int Health { get; set; }
	}
}
