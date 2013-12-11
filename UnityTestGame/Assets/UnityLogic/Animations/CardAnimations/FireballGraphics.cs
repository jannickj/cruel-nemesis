using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnityLogic.Unit.Animations
{
	class FireballGraphics : UnitGraphic
	{
        public FireballGraphics()
        {
    
        }

        public override void LoadAnimations()
        {
            var resolves_ani = new TextureAnimation("fireball", 8, 8);
            resolves_ani.Frames = Enumerable.Range(1, 38).ToArray();
            resolves_ani.FrameRepeats = Enumerable.Repeat<int>(1, 38).ToArray();
            this.SetAnimation(StandardUnitAnimations.Idle, resolves_ani);
        }
	}
}
