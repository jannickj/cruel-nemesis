using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Gui.Controls
{
    public class GUIButtonHandler : MonoBehaviour
	{
        public event EventHandler MouseDownEvent;


        void OnMouseDown()
        {
            
            if (MouseDownEvent != null)
                MouseDownEvent(this, new EventArgs());
        }
	}
}
