using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Gui
{
	public class GUITextureAutoScaler : MonoBehaviour
	{
        private static int BaseHeight = 1920;
        private static int BaseWidth = 1200;
        private GUITexture myGUITexture;

        void Awake()
        {
            myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
        }

        // Use this for initialization
        void Start()
        {
            // Position the billboard in the center, 
            // but respect the picture aspect ratio
            int textureHeight = guiTexture.texture.height;
            int textureWidth = guiTexture.texture.width;
            int screenHeight = Screen.height;
            int screenWidth = Screen.width;

            float ratioHeight = ((float)screenHeight) / ((float)BaseHeight);
            float ratioWidth = ((float)screenWidth) / ((float)BaseWidth);

            float ratio = ratioHeight > ratioWidth ? ratioHeight : ratioWidth;

            
            Rect curPos = myGUITexture.pixelInset;

            float xPos = 0;
            float yPos = 0;

            float height = 0;
            float width = 0;

            myGUITexture.pixelInset =
                new Rect(xPos, yPos,
                width, height);
            
        }
	}
}
