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
        private GUITexture myGUITexture;
        //bool initSize = false;
        public Rect CurSize { get; set; }
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
            myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
            CurSize = myGUITexture.pixelInset;
            
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

            

            float xPos = CurSize.x*ratio;
            float yPos = CurSize.y * ratio;

            float height = CurSize.height * ratio;
            float width = CurSize.width * ratio;
            
            myGUITexture.pixelInset = new Rect(xPos, yPos, width, height);
               
            
        }
	}
}
