using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.Map.Terrain;
using UnityEngine;
using System.IO;
using Cruel.GameLogic.Unit;

namespace Assets.UnityLogic
{
	public class TextureDictionary
	{
        private static Dictionary<string, Texture> loadedtexdic = new Dictionary<string, Texture>();
        
        static TextureDictionary()
        {
            
        }   

        public static Texture GetTexture(TerrainTypes type)
        {
            
            return loadedtexdic["terrain_"+type.ToString().ToLower()];
        }

        public static Texture GetTexture(string textureid)
        {
            return loadedtexdic[textureid];
        }

        public static void LoadTextures()
        {
            string path = "Resources/Textures";
            //Debug.Log("loading textures");
            string fullpath = Application.dataPath+"/"+path;
            string[] files = Directory.GetFiles(fullpath,"*.*",SearchOption.AllDirectories);
           
            foreach (string f in files)
            {
                string newf = f.Replace("\\", "/").Replace(Application.dataPath+"/Resources/","");
                newf = newf.Substring(0,newf.LastIndexOf("."));
                //Debug.Log(newf);
                Texture tex = Resources.Load(newf) as Texture2D;
                //Debug.Log(tex);
                if (tex == null)
                    continue;
                newf = newf.Replace("Textures/", "").Replace("/", "_").ToLower();
                //Debug.Log(newf);

                loadedtexdic.Add(newf, tex);
            }
        }

	}
}
