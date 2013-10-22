using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using JSLibrary.Data;

namespace Assets.UnityLogic.Unit
{
    public class HealthbarView : MonoBehaviour
	{


        void Update()
        {
            Camera m_Camera = Camera.main;
            //var rot = transform.rotation;
            
            //transform.LookAt(m_Camera.transform);
            //var newrot = transform.rotation;
            //var fixedrot = new Quaternion(newrot.x, rot.y, rot.z, rot.w);
            //transform.rotation = fixedrot;
            //transform.Rotate(90, 0, 0);

            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back,
            m_Camera.transform.rotation * Vector3.up);
            transform.Rotate(90, 0, 0);
        }

        public void SetPosition(Point p)
        {
            this.transform.position = new Vector3(-((float)p.X), ((float)p.Y)+1.2f, 0.7f);
        }

        public void SetHealthPct(float pct)
        {
            this.renderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2((pct*0.5f)/100,0));
        }
	}
}
