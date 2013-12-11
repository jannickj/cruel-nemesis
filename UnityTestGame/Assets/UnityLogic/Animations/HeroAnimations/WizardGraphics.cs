using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.CardAnimations
{
	public class WizardGraphics : UnitGraphic
	{
        public WizardGraphics()
        {
    
        }

        public override void LoadAnimations()
        {
            TextureAnimation ani;
            ani = new TextureAnimation("heroes_wizard_idle", 6,5);
            ani.Frames = Enumerable.Range(1, 30).ToArray();
            ani.FrameRepeats = Enumerable.Repeat<int>(3, 30).ToArray();
            this.SetAnimation(StandardUnitAnimations.Idle, ani);

            ani = new TextureAnimation("heroes_wizard_walking", 4, 3);
            ani.Frames = Enumerable.Range(1, 12).ToArray();
            ani.FrameRepeats = Enumerable.Repeat<int>(3, 12).ToArray();
            this.SetAnimation(StandardUnitAnimations.Move, ani);
        }
    }
}
