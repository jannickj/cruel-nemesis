using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using JSLibrary.Data;

namespace Assets.UnityLogic.Unit
{
    public class HealthbarView : SpriteView
	{



        public void SetPosition(Point p)
        {
            this.transform.position = new Vector3(-((float)p.X)-0.1f, ((float)p.Y)+1.3f, 0.8f);
        }

        public void SetHealthPct(float pct)
        {
            var val = 1f-((pct*0.5f)/100f);
            this.gameObject.renderer.material.SetTextureOffset("_MainTex", new Vector2(val,0));
        }
	}
}
