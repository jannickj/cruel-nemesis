using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;
using UnityEngine;

namespace Assets.UnityLogic.Animations.UnitAnimations
{
	public class ArcherGraphics : UnitGraphics
	{
        public ArcherGraphics()
        {
    
        }

        protected override void PrepareUnitAnimations()
        {
            TextureAnimation ani;
            Texture tex;
            tex = Factory.LoadUnitTexture("archer_walking");
            ani = new TextureAnimation(tex, 4, 3);
            ani.Frames = Enumerable.Range(1, 12).ToArray();
            ani.FrameRepeats = Enumerable.Repeat<int>(2, 12).ToArray();
            this.SetUnitAnimation(StandardUnitAnimations.Move, ani);

        }


        public override TextureAnimation GenerateIdleAnimation()
        {
            TextureAnimation ani;
            Texture tex;
            tex = Factory.LoadUnitTexture("archer_idle");
            ani = new TextureAnimation(tex, 6, 4);
            ani.Frames = Enumerable.Range(1, 21).ToArray();
            ani.FrameRepeats = Enumerable.Repeat<int>(4, 21).ToArray();
            return ani;
        }
    }
}
