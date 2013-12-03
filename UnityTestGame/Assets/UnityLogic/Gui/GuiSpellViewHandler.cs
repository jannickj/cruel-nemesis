using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Gui
{
	public class GuiSpellViewHandler : MonoBehaviour
	{
        public Vector3 FlyToPos { get; set; }

        public void Start()
        {
            this.gameObject.transform.position = FlyToPos;
        }
	}
}
