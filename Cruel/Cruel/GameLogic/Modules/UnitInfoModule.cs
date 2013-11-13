using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.Modules
{
	public class UnitInfoModule : UniversalModule<XmasEntity>
	{
        private Player owner;
        private Player controller;

        public Player Owner 
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            } 
        }

        public Player Controller 
        {
            get
            {
                return controller;
            }
            set
            {
                controller = value;
            }
        }


        public UnitInfoModule(Player owner)
        {
            this.owner = owner;
            this.controller = owner;
        }
    }
}
