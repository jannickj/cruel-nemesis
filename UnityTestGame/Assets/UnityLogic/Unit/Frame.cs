using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Unit
{
    public struct Frame
    {
        private Vector2 offSet;
        private Vector2 size;
        private Texture tex;

        public Frame(Vector2 offSet, Vector2 size, Texture tex)
        {
            this.offSet = offSet;
            this.size = size;
            this.tex = tex;
        }


        public Vector2 OffSet
        {
            get { return offSet; }
        }
        
        public Vector2 Size
        {
            get { return size; }
        }

        public Texture Texture 
        { 
            get 
            { 
                return tex; 
            } 
        } 
    }
}
