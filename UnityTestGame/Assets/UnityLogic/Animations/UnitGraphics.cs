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
        private Dictionary<StandardUnitAnimations, GameObjectAnimation> animations = new Dictionary<StandardUnitAnimations, GameObjectAnimation>();
        private GameObject unit;

        public UnitGraphics()
        {
            this.AutoDelete = false;
        }

        public void SetUnitObj(GameObject unit)
        {
            this.unit = unit;
        }

        protected sealed override void Prepare()
        {
            SetUnitAnimation(StandardUnitAnimations.Idle, GenerateIdleAnimation());
            SetUnitAnimation(StandardUnitAnimations.Death, GenerateDeathAnimation());
            PrepareUnitAnimations();
        }

        protected virtual void PrepareUnitAnimations()
        {
        }

        protected virtual GameObjectAnimation GenerateDeathAnimation()
        {
            var seq_ani = new SequenceAnimation();
            var death_ani = new TextureAnimation(this.Factory.LoadUnitTexture("death_default"), 5, 3);
            death_ani.Frames = Enumerable.Range(1, 15).ToArray();
            death_ani.FrameRepeats = Enumerable.Repeat<int>(2, 15).ToArray();
      
            seq_ani.AddAnimation(death_ani);

            var dead_ani = new TextureAnimation(this.Factory.LoadUnitTexture("death_default"), 5, 3);
            dead_ani.Frames = new int[] { 15 };
            dead_ani.FrameRepeats = Enumerable.Repeat<int>(2, 1).ToArray();
            dead_ani.AutoLooping = true;
            seq_ani.AddAnimation(dead_ani);


            return seq_ani;
        }



        public abstract TextureAnimation GenerateIdleAnimation();
        

        

        protected void SetUnitAnimation(StandardUnitAnimations aniId, GameObjectAnimation ani)
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


        public GameObjectAnimation GetAnimation(StandardUnitAnimations ani)
        {
            return animations[ani];
        }

        public GameObjectAnimation[] Animations 
        {
            get
            {
                return animations.Values.ToArray();
            }
            
        }
    }
}
