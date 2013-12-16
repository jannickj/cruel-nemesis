using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension;
using Cruel.Map.Terrain;
using JSLibrary.Data;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations.SpellAnimations
{
	public class RaiseDeadGraphics : SpellGraphics
	{
        public RaiseDeadGraphics()
        {
    
        }

        protected override void Prepare()
        {
            var effect = Factory.Create1by1Sprite();
            float size = 0.4f;
            effect.transform.localScale = new UnityEngine.Vector3(size, size, size);
            var terrain = (TerrainEntity)Spell.Targets[0][0];
            var toPos = Factory.ConvertPos(terrain.PositionAs<TilePosition>().Point, 0.8f);
            effect.transform.position = toPos;
            ParallelAnimation parallelani = new ParallelAnimation();
            TextureAnimation texani = new TextureAnimation(this.Factory.LoadSpellTexture("raise_dead"), 5, 6);
            texani.Frames = Enumerable.Range(1, 30).ToArray();
            texani.FrameRepeats = Enumerable.Repeat<int>(1, 30).ToArray();
            parallelani.Add(texani, true);

            SizeChangeAnimation sizeani = new SizeChangeAnimation(0.02f);
            parallelani.Add(sizeani, false);
            this.EnqueueAnimation(effect, parallelani);
        }
	}
}
