using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.SpellAnimations
{
	public class InspirationGraphics : SpellGraphics
	{
        public InspirationGraphics()
        {
    
        }

        protected override void Prepare()
        {
            var effect = Factory.Create1by1Sprite();
            effect.transform.position = Factory.ConvertPos(Spell.Controller.Hero.PositionAs<TilePosition>().Point, 0.8f);
            ParallelAnimation parallelani = new ParallelAnimation();
            TextureAnimation texani = new TextureAnimation(this.Factory.LoadSpellTexture("inspiration"), 5, 5);
            texani.Frames = Enumerable.Range(6, 20).ToArray();
            texani.FrameRepeats = new int[] { 0, 0, 0, 0, 0, 
                                              1, 1, 1, 1, 1, 
                                              1, 1, 1, 1, 1, 
                                              1, 1, 1, 1, 1 };
            parallelani.Add(texani, true);

            SizeChangeAnimation sizeani = new SizeChangeAnimation(0.02f);
            parallelani.Add(sizeani, false);
            this.EnqueueAnimation(effect, parallelani);
        }
	}
}
