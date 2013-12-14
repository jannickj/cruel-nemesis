using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Gui
{
	public class GUITextureAutoScaler : MonoBehaviour
	{
        private static int BaseHeight = 1080;
        private static int BaseWidth = 1920;
        private Action<Rect> setRectFunc;
        private Func<Rect> getRectFunc;
        //bool initSize = false;
        public Rect CurPlacement { get; set; }
        //public Rect CurSize
        //{
        //    get
        //    {
        //        if (!initSize)
        //        {
        //            myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
        //            curSize = myGUITexture.pixelInset;
        //            initSize = true;
        //        }
        //        return curSize;
        //    }
        //    set
        //    {
        //        curSize = value;
        //    }

        //}
        

        void Awake()
        {
            //var curSize = CurSize;
            //Debug.Log();
            //this.gameObject.GetComponent<GUIText>().p
            var guitexture = this.gameObject.GetComponent<GUITexture>();
            if (guitexture != null)
            {
                this.setRectFunc = rect => guitexture.pixelInset = rect;
                this.getRectFunc = () => guitexture.pixelInset;
            }

            var guitext = this.gameObject.GetComponent<GUIText>();
            if (guitext != null)
                this.setRectFunc = rect => guitext.pixelOffset = new Vector2(rect.x, rect.y);

            //myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
            if (getRectFunc != null)
                CurPlacement = getRectFunc();
            
        }

        // Use this for initialization
        void Update()
        {
            //var size = myGUITexture.pixelInset;
            //if(size.height != this.lastChangedTo.height)
            //    curSize.height = size.height;
            //if(size.width != this.lastChangedTo.width)
            //    curSize.width = size.width;
            //if(size.x != this.lastChangedTo.x)
            //    curSize.x = size.x;
            //if(size.y != this.lastChangedTo.y)
            //    curSize.y = size.y;

            // Position the billboard in the center, 
            // but respect the picture aspect ratio
            int screenHeight = Screen.height;
            int screenWidth = Screen.width;

            float ratioHeight = ((float)screenHeight) / ((float)BaseHeight);
            float ratioWidth = ((float)screenWidth) / ((float)BaseWidth);

            float ratio = ratioHeight > ratioWidth ? ratioWidth : ratioHeight;

            

            float xPos = CurPlacement.x*ratio;
            float yPos = CurPlacement.y * ratio;

            float height = CurPlacement.height * ratio;
            float width = CurPlacement.width * ratio;
            if(setRectFunc!=null)
                this.setRectFunc(new Rect(xPos, yPos, width, height));
            
        }
	}
}
