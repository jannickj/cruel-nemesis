using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.CardAnimations
{
	public class WarhoundGraphics : UnitGraphic
	{
        public WarhoundGraphics()
        {
    
        }

        public override void LoadAnimations()
        {
            var idle_ani = new TextureAnimation("warhound", 1,1);
            idle_ani.Frames = Enumerable.Range(1, 1).ToArray();
            idle_ani.FrameRepeats = Enumerable.Repeat<int>(50, 1).ToArray();
            this.SetAnimation(StandardUnitAnimations.Idle, idle_ani);
        }
    }
}
