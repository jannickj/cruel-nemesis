using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using UnityEngine;
using Assets.UnityLogic.Gui;

namespace Assets.UnityLogic.Unit
{
	public class UnitInformation : MonoBehaviour
	{
        private UnitGraphic graphics;
        private UnitEntity entity;

        public UnitGraphic Graphics
        {
            get { return graphics; }
            
        }

        public UnitEntity Entity
        {
            get { return entity; }
        }
        
        public void SetGraphics(UnitGraphic graphics)
        {
            this.graphics = graphics;
        }

        public void SetEntity(UnitEntity entity)
        {
            this.entity = entity;
        }

        public GuiInformation ControllerInfo { get; set; }
    }
}
