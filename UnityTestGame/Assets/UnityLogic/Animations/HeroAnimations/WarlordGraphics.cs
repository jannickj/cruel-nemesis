using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;
using UnityEngine;

namespace Assets.UnityLogic.Animations.CardAnimations
{
	public class WarlordGraphics : UnitGraphics
	{
        public WarlordGraphics()
        {
    
        }

        protected override void  PrepareUnitAnimations()
        {
            TextureAnimation ani;
            Texture tex;
            tex = Factory.LoadHeroTexture("warlord_walking");
            ani = new TextureAnimation(tex, 5, 3);
            ani.Frames = Enumerable.Range(1, 14).ToArray();
            ani.FrameRepeats = Enumerable.Repeat<int>(3, 14).ToArray();
            this.SetUnitAnimation(StandardUnitAnimations.Move, ani);
        }

        public override TextureAnimation GenerateIdleAnimation()
        {
            TextureAnimation ani;
            Texture tex;
            tex = Factory.LoadHeroTexture("warlord_idle");
            ani = new TextureAnimation(tex, 4, 4);
            ani.Frames = Enumerable.Range(1, 16).ToArray();
            ani.FrameRepeats = Enumerable.Repeat<int>(3, 16).ToArray();
            return ani;
        }
    }
}
