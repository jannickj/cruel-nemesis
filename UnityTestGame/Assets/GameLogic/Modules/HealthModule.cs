using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic.Modules
{
	public class HealthModule : UniversalModule<XmasEntity>
	{

        public HealthModule(int maxhealth)
        {
            this.Health = maxhealth;
            this.MaxHealth = maxhealth;
        }

        public int Health 
        { 
            get; 
            set; 
        }

        public int MaxHealth
        {
            get;
            set;
        }


        public float HealthPct 
        {
            get
            {
                return ((float)Health) / ((float)MaxHealth);
            }
        }
    }
}
