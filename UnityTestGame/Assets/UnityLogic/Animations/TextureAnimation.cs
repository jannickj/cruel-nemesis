using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.UnityLogic.Animations;

namespace Assets.UnityLogic.Unit
{
	public class TextureAnimation : GameObjectAnimation
	{
        private Texture texture;
        private int frameIndex = 0;
        private int frameRepeated = 0;
        private Vector2 TileSize;
        private int columns;
        private int rows;

        public TextureAnimation(Texture tex, int columns, int rows)
        {
            this.texture = tex;
            this.columns = columns;
            this.rows = rows;

            TileSize = new Vector2(1.0f / (float)columns, 1.0f / (float)rows);
        
        }

        public int[] Frames { get; set; }

        public int[] FrameRepeats { get; set; }

        /// <summary>
        /// Moves the animation to the next frame if the frame was the last frame false is returned
        /// </summary>
        /// <returns></returns>
        private bool NextFrame()
        {
            frameRepeated++;
            if (FrameRepeats[frameIndex] < frameRepeated)
            {
                frameIndex++;
                frameRepeated = 0;
                if (frameIndex >= Frames.Length)
                {
                    frameIndex = 0;
                    return false;
                }
            }
            return true;
        }

        public Frame CurrentFrame()
        {

            int uindex = frameIndex % columns;
            int vindex = frameIndex / columns;

            float offx = uindex * TileSize.x;
            float offy = 1.0f - TileSize.y - vindex * TileSize.y;
            Vector2 OffSet = new Vector2(offx,offy);

            return new Frame(OffSet, TileSize,this.texture);
        }



        public float HeightToWidthAspect(float height)
        {
            float iH = texture.height/(float)this.rows;
            float iW = texture.width/(float)this.columns;
            float aspect = iW/iH;
            return height * aspect;
        }

        protected override void ResetInternal()
        {
            frameIndex = 0;
            frameRepeated = 0;
            
        }

        private void setFrame(GameObject obj, Frame f)
        {

            obj.renderer.material.SetTexture("_MainTex", f.Texture);
            obj.renderer.material.SetTextureOffset("_MainTex", f.OffSet);
            obj.renderer.material.SetTextureScale("_MainTex", f.Size);

        }

        protected override void InternalSetup(GameObject gobj)
        {
            setFrame(gobj, CurrentFrame());
        }

        protected override bool UpdateInternal(GameObject obj)
        {
            var scale = obj.transform.localScale;
            var newX = HeightToWidthAspect(scale.z);
            var newZ = scale.z;
            var settings = obj.GetComponent<MirrorSettingsView>();
            if(settings!=null)
                if(settings.HorizontalMirroring)
                    newX = newX * -1;

            
            obj.transform.localScale = new Vector3(newX, scale.y, scale.z);
            var lastFrame = NextFrame();

            setFrame(obj, CurrentFrame());

            return !lastFrame;
        }
    }
}
