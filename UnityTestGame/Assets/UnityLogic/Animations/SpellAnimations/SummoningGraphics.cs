using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Animations;
using Cruel.Map.Terrain;
using XmasEngineExtensions.TileExtension;

namespace Assets.UnityLogic.Unit.Animations
{
	class SummoningGraphics : SpellGraphics
	{
        public SummoningGraphics()
        {
    
        }

        protected override void Prepare()
        {
           
            var effect = Factory.Create1by1Sprite();
            float effectSize = 0.2f;
            effect.transform.localScale = new UnityEngine.Vector3(effectSize,effectSize,effectSize);
            TextureAnimation texani;
            var terrain = (TerrainEntity)Spell.Targets[0][0];
            var pos = Factory.ConvertPos(terrain.PositionAs<TilePosition>().Point, 1f);
            effect.transform.position = pos;
            
            texani = new TextureAnimation(this.Factory.LoadSpellTexture("summoning"), 5, 6);
            texani.Frames = Enumerable.Range(1, 30).ToArray();
            texani.FrameRepeats = Enumerable.Repeat<int>(2, 30).ToArray();
            this.EnqueueAnimation(effect, texani);
            
            
            
        }
	}
}
