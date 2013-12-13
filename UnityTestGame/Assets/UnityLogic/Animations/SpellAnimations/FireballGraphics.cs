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
            ParallelAnimation parallelani;
            var ball = Factory.Create1by1Sprite();
            float ballSize = 0.5f;
            ball.transform.localScale = new UnityEngine.Vector3(ballSize,ballSize,ballSize);
            TextureAnimation texani;
            SizeChangeAnimation sizeani;
            MoveTransformAnimation moveani;
            var height = 1f;
            var terrain = (TerrainEntity)Spell.Targets[0][0];
            var heropos = Spell.Controller.Hero.PositionAs<TilePosition>().Point;
            var fromPos = Factory.ConvertPos(heropos, height);
            var toPos = Factory.ConvertPos(terrain.PositionAs<TilePosition>().Point, 0.8f);

            parallelani = new ParallelAnimation();
            texani = new TextureAnimation(this.Factory.LoadSpellTexture("fireball"), 8, 8);
            texani.Frames = new int[]{6};
            texani.FrameRepeats = Enumerable.Repeat<int>(1, 2).ToArray();
            texani.AutoLooping = true;
            parallelani.Add(texani, false);
            
            moveani = new MoveTransformAnimation(fromPos,toPos,12f);
            parallelani.Add(moveani,  true);
            this.EnqueueAnimation(ball, parallelani);

            parallelani.Completed += (sender, evt) =>
                {
                    var explosion = Factory.Create1by1Sprite();
                    explosion.transform.position = toPos;
                    parallelani = new ParallelAnimation();
                    texani = new TextureAnimation(this.Factory.LoadSpellTexture("fireball"), 8, 8);
                    texani.Frames = Enumerable.Range(1, 38).ToArray();
                    texani.FrameRepeats = Enumerable.Repeat<int>(1, 38).ToArray();
                    parallelani.Add(texani, true);

                    sizeani = new SizeChangeAnimation(0.02f);
                    parallelani.Add(sizeani, false);
                    this.EnqueueAnimation(explosion, parallelani);
                };
        }
	}
}
