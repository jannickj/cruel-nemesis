using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic
{
	public class Player : XmasUniversal
	{
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return "player("+name+")";
        }
	}
}
