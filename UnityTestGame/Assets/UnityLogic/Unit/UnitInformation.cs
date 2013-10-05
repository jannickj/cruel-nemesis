using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.Unit;
using UnityEngine;

namespace Assets.UnityLogic.Unit
{
	public class UnitInformation : MonoBehaviour
	{
        private UnitGraphics graphics;
        private UnitEntity entity;

        public UnitGraphics Graphics
        {
            get { return graphics; }
            
        }

        public UnitEntity Entity
        {
            get { return entity; }
        }
        
        public void SetGraphics(UnitGraphics graphics)
        {
            this.graphics = graphics;
        }

        public void SetEntity(UnitEntity entity)
        {
            this.entity = entity;
        }
	}
}
