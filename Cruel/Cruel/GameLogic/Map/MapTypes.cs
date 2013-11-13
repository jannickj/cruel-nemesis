﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cruel.Map
{
    /// <summary>
    /// All map types of the engine
    /// </summary>
	public enum MapTypes
	{
        [MapType(typeof(StandardGameMapBuilder))]
        Standard
	}


    /// <summary>
    /// Class for storing the map type in an enum field, is only there so you can select map types in unity
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MapTypeAttribute : Attribute
    {
        public Type Type { get; private set; }

        public MapTypeAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
