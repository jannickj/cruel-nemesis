using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic
{
    public class SpriteView : MonoBehaviour
	{
        private bool spriteMode = true;

        public bool SpriteMode
        {
            get { return spriteMode; }
            set { spriteMode = value; }
        }

        void Update()
        {
            if (spriteMode)
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
        }
	}
}
