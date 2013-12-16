using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.UnitAnimations
{
	public class DragonGraphics : UnitGraphics
	{
        public DragonGraphics()
        {
    
        }



        public override TextureAnimation GenerateIdleAnimation()
        {
            var idle_ani = new TextureAnimation(this.Factory.LoadUnitTexture("dragon_idle2"), 3, 2);
            idle_ani.Frames = Enumerable.Range(1, 6).ToArray();
            idle_ani.FrameRepeats = Enumerable.Repeat<int>(50, 6).ToArray();
            return idle_ani;
        }
    }
}
