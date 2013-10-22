using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;

namespace Assets.GameLogic.Modules
{
	public class AttackModule : UniversalModule<XmasEntity>
	{
        public int AttackRange { get; set; }
        public int Damage { get; set; }

        public int AttackTime { get; set; }

        public int CalculateDamage()
        {
            return Damage;
        }
    }
}
