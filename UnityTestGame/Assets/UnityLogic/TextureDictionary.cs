using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Map.Terrain;
using UnityEngine;

namespace Assets.UnityLogic
{
	public class TextureDictionary
	{
        private static Dictionary<TerrainTypes, string> texdic = new Dictionary<TerrainTypes, string>();
        private static Dictionary<TerrainTypes, Texture2D> loadedtexdic = new Dictionary<TerrainTypes, Texture2D>();
        
        static TextureDictionary()
        {
            texdic.Add(TerrainTypes.Default,"prototype_map_tex");
        }   

        public static Texture2D GetTexture(TerrainTypes type)
        {
            return loadedtexdic[type];
        }

        public static void LoadTexturesFrom(string path)
        {
            foreach (KeyValuePair<TerrainTypes, string> kv in texdic)
            {
				var fullpath = path + "/" + kv.Value;
                object tex = Resources.Load(fullpath);
                loadedtexdic.Add(kv.Key, (Texture2D)tex);
            }
        }

	}
}
