using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.UnityLogic.Animations;

namespace Assets.UnityLogic.Unit
{
	public abstract class UnitGraphics : GameGraphics
	{
        private Dictionary<StandardUnitAnimations, TextureAnimation> animations = new Dictionary<StandardUnitAnimations, TextureAnimation>();
        private GameObject unit;

        public UnitGraphics()
        {
            this.AutoDelete = false;
        }

        public void SetUnitObj(GameObject unit)
        {
            this.unit = unit;
        }
        

        

        protected void SetUnitAnimation(StandardUnitAnimations aniId, TextureAnimation ani)
        {
            this.animations[aniId] = ani;
        }

        public bool HasUnitAnimation(StandardUnitAnimations ani)
        {
            return animations.ContainsKey(ani);
        }

        public void UseUnitAnimation(StandardUnitAnimations ani)
        {
            var uani = GetAnimation(ani);
            uani.AutoLooping = true;
            this.OverwriteAnimation(unit, uani);
        }


        public TextureAnimation GetAnimation(StandardUnitAnimations ani)
        {
            return animations[ani];
        }

        public TextureAnimation[] Animations 
        {
            get
            {
                return animations.Values.ToArray();
            }
            
        }
    }
}
