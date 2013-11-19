using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
    public class CardViewHandler : MonoBehaviour
	{
        public Texture Texture { get; set; }
        public GameCard Card { get; set; }

        void Start()
        {
            this.renderer.material.SetTexture("_MainTex", Texture);
        }
	}
}
