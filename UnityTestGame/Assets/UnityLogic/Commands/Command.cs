using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnityLogic.Commands
{
	public abstract class Command
	{

        public PlayerController PlayerController { get; set; }
        public abstract void Update();

        public bool Finished { get; protected set; }
    }
}
