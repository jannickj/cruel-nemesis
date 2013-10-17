using System;
using System.Collections.Generic;
using XmasEngineModel;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Percepts;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class SpeedModule : UniversalModule
	{
		private double speed;

		public double Speed {
			get { return speed; }
			set { speed = value; }
		}

		public override IEnumerable<Percept> Percepts
		{
			get { return new Percept[] {new SingleNumeralPercept ("speed", speed)}; }
		}

		public SpeedModule (double speed)
		{
			this.speed = speed;
		}
	}
}

