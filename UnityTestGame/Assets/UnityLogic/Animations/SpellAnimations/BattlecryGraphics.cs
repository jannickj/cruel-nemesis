using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Unit;
using XmasEngineExtensions.TileExtension;

namespace Assets.UnityLogic.Animations.SpellAnimations
{
	public class BattlecryGraphics : SpellGraphics
	{
        public BattlecryGraphics()
        {
    
        }

        protected override void Prepare()
        {
            var effect = Factory.Create1by1Sprite();
            float size = 0.4f;
            effect.transform.localScale = new UnityEngine.Vector3(size, size, size);
            effect.transform.position = Factory.ConvertPos(Spell.Controller.Hero.PositionAs<TilePosition>().Point, 0.8f);
            ParallelAnimation parallelani = new ParallelAnimation();
            TextureAnimation texani = new TextureAnimation(this.Factory.LoadSpellTexture("inspiration"), 5, 4);
            texani.Frames = Enumerable.Range(1, 20).ToArray();
            texani.FrameRepeats = new int[] { 1, 1, 1, 1, 1, 
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
