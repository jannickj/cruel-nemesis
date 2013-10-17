using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic.Modules
{
	public class MoveModule : UniversalModule<XmasEntity>
	{
        private int moveLength;

        public int MoveLength
        {
            get
            {
                return moveLength;
            }
        }

        public MoveModule(int movelength)
        {
            this.moveLength = movelength;
        }

        public void ChangeMoveLength(int len)
        {
            this.moveLength = len;
        }
        
	}
}
