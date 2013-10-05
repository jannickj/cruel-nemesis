using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Unit
{
	public class UnitAnimation
	{
        private string textureId;
        private Texture texture;
        private int frameIndex = 0;
        private int frameRepeated = 0;
        private int SubImageWidth;
        private int SubImageHeight;
        private Vector2 TileSize;
        private int columns;
        private int rows;

        public UnitAnimation(string textureId, int columns, int rows)
        {
            this.textureId = textureId;
            this.columns = columns;
            this.rows = rows;

            TileSize = new Vector2(1.0f / (float)columns, 1.0f / (float)rows);
        
        }

        public int[] Frames { get; set; }

        public int[] FrameRepeats { get; set; }

        /// <summary>
        /// Resets the frame
        /// </summary>
        public void Reset()
        {
            frameIndex = 0;
            frameRepeated = 0;
        }

        /// <summary>
        /// Moves the animation to the next frame if the frame was the last frame false is returned
        /// </summary>
        /// <returns></returns>
        public bool NextFrame()
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

        public void SetTexture(Texture tex)
        {
            this.texture = tex;
        }


        public string TextureId
        {
            get { return textureId; }
        }
    }
}
