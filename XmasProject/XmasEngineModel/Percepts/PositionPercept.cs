using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.World;

namespace XmasEngineModel.Percepts
{
	public class PositionPercept : Percept
	{
		private XmasPosition position;


		public XmasPosition Position
		{
			get { return position; }
		}

		public PositionPercept(XmasPosition position)
		{
			this.position = position;
		}

	}
}
