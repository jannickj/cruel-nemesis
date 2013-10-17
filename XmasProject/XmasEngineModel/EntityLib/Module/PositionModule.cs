using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Percepts;

namespace XmasEngineModel.EntityLib.Module
{

	/// <summary>
	/// A module for giving the entity a Position percept
	/// </summary>
	public class PositionModule : UniversalModule<XmasEntity>
	{
		public override Type ModuleType
		{
			get
			{
				return typeof(PositionModule);
			}
		}

		public override IEnumerable<Percept> Percepts
		{
			get
			{

				return new Percept[]{ new PositionPercept(this.Host.Position)};
			}
		}
	}
}
