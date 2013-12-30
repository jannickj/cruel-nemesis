using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension;

namespace Cruel.GameLogic.Modules
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

        public bool CanReachPoint(Point from, Point to)
        {
            var pointP = from;
            int difx = Math.Abs(pointP.X - to.X);
            int dify = Math.Abs(pointP.Y - to.Y);
            int attackrange = AttackRange;
            return attackrange >= difx && attackrange >= dify;
        }
    }
}
