﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension;
using Cruel.Map.Terrain;
using Assets.UnityLogic.Unit;
using Cruel.GameLogic.Unit;
using XmasEngineModel.EntityLib;

namespace Assets.UnityLogic.Animations.SpellAnimations
{
	public class LightningBoltGraphics : SpellGraphics
	{
        public LightningBoltGraphics()
        {
    
        }

        protected override void Prepare()
        {
            var effect = Factory.Create1by1Sprite();
            float size = 0.3f;
            effect.transform.localScale = new UnityEngine.Vector3(size, size, size);
            var target = ((XmasEntity)Spell.Targets[0][0]);
            var pos = Factory.ConvertPos(target.PositionAs<TilePosition>().Point, 0.8f);
            pos.y += 0.5f;
            effect.transform.position = pos;
            ParallelAnimation parallelani = new ParallelAnimation();
            TextureAnimation texani = new TextureAnimation(this.Factory.LoadSpellTexture("lightningbolt"), 5, 5);
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
