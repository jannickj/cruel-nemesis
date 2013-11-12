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
        Rect curSize;

        void Awake()
        {
            myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
            curSize = myGUITexture.pixelInset;
        }

        // Use this for initialization
        void Update()
        {
            // Position the billboard in the center, 
            // but respect the picture aspect ratio
            int screenHeight = Screen.height;
            int screenWidth = Screen.width;

            float ratioHeight = ((float)screenHeight) / ((float)BaseHeight);
            float ratioWidth = ((float)screenWidth) / ((float)BaseWidth);

            float ratio = ratioHeight > ratioWidth ? ratioWidth : ratioHeight;

            

            float xPos = curSize.x*ratio;
            float yPos = curSize.y * ratio;

            float height = curSize.height * ratio;
            float width = curSize.width * ratio;
            
            

            myGUITexture.pixelInset =
                new Rect(   xPos,   yPos,
                            width,  height);
            
        }
	}
}
