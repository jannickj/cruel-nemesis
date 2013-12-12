using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Animations;
using Cruel.Map.Terrain;
using XmasEngineExtensions.TileExtension;

namespace Assets.UnityLogic.Unit.Animations
{
	class FireballGraphics : SpellGraphics
	{
        public FireballGraphics()
        {
    
        }

        protected override void Prepare()
        {
            var parallelani = new ParallelAnimation();
            var texani = new TextureAnimation(this.Factory.LoadSpellTexture("fireball"), 8, 8);
            texani.Frames = Enumerable.Range(1, 38).ToArray();
            texani.FrameRepeats = Enumerable.Repeat<int>(1, 38).ToArray();
            parallelani.Add(texani, true);

            var sizeani = new SizeChangeAnimation(0.02f);
            parallelani.Add(sizeani, false);

            var gobj = Factory.Create1by1Squre();

            var terrain = (TerrainEntity)Spell.Targets[0][0];
            gobj.transform.position = Factory.ConvertPos(terrain.PositionAs<TilePosition>().Point,0.01f);

            this.EnqueueAnimation(gobj, parallelani);
            
        }
	}
}
