using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.UnitAnimations
{
    public class GruntGraphics : UnitGraphics
    {
        public GruntGraphics()
        {
            
        }

        protected override void Prepare()
        {
            var idle_ani = new TextureAnimation(this.Factory.LoadUnitTexture("grunt_idle"), 3, 3);
            idle_ani.Frames = Enumerable.Range(1, 6).ToArray();
            idle_ani.FrameRepeats = Enumerable.Repeat<int>(50, 6).ToArray();
            this.SetUnitAnimation(StandardUnitAnimations.Idle, idle_ani);
        }

        
    }
}
