using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Unit
{
	public abstract class UnitGraphics
	{
        private Dictionary<StandardUnitAnimations, UnitAnimation> animations = new Dictionary<StandardUnitAnimations, UnitAnimation>();

     

        public abstract void LoadAnimations();

        protected void SetAnimation(StandardUnitAnimations aniId, UnitAnimation ani)
        {
            this.animations[aniId] = ani;
        }



        public UnitAnimation GetAnimation(StandardUnitAnimations ani)
        {
            return animations[ani];
        }

        public UnitAnimation[] Animations 
        {
            get
            {
                return animations.Values.ToArray();
            }
            
        }
    }
}
