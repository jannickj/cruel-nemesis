using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.UnitAnimations
{
	public class BruteGraphics : UnitGraphics
	{
        public BruteGraphics()
        {
    
        }

        protected override void Prepare()
        {
            var idle_ani = new TextureAnimation(this.Factory.LoadUnitTexture("brute"), 1,1);
            idle_ani.Frames = Enumerable.Range(1, 1).ToArray();
            idle_ani.FrameRepeats = Enumerable.Repeat<int>(50, 1).ToArray();
            this.SetUnitAnimation(StandardUnitAnimations.Idle, idle_ani);
        }
    }
}
