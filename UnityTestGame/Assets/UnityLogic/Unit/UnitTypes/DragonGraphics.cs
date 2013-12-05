﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnityLogic.Unit.UnitTypes
{
	public class DragonGraphics : UnitGraphics
	{
        public DragonGraphics()
        {
    
        }

        public override void LoadAnimations()
        {
            var idle_ani = new UnitAnimation("dragon_idle", 3, 3);
            idle_ani.Frames = Enumerable.Range(1, 6).ToArray();
            idle_ani.FrameRepeats = Enumerable.Repeat<int>(50, 6).ToArray();
            this.SetAnimation(StandardUnitAnimations.Idle, idle_ani);
        }
    }
}
