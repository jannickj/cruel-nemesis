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
        private int moveDuration;

        public int MoveLength
        {
            get
            {
                return moveLength;
            }
        }

        public int MoveDuration
        {
            get
            {
                return this.moveDuration;
            }
        }

        public MoveModule(int movelength, int moveDuration = 200)
        {
            this.moveLength = movelength;
            this.moveDuration = moveDuration;
        }

        public void ChangeMoveLength(int len)
        {
            this.moveLength = len;
        }

        public void ChangeMoveDuration(int dur)
        {
            this.moveLength = dur;
        }
        
	}
}
