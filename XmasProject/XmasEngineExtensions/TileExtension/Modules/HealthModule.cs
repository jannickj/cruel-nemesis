using System;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel;
using System.Collections.Generic;
using XmasEngineModel.Percepts;
using System.Linq;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class HealthModule : UniversalModule
	{
		private int health;

		public int Health
		{
			get { return health; }
			set { health = value; }
		}

		public HealthModule (int health)
		{
			this.health = health;
		}

		public override IEnumerable<Percept> Percepts {
			get {
				return new Percept[] { new SingleNumeralPercept("health", health) };
			}
		}
	}
}

