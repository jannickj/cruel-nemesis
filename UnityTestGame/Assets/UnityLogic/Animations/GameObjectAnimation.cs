using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Animations
{
	public abstract class GameObjectAnimation 
	{
        public event EventHandler Completed;
        public event EventHandler Begining;
        private bool firstUpdate = true;


        public void Reset()
        {
            firstUpdate = true;
            ResetInternal();
        }

        protected abstract void ResetInternal();

        /// <summary>
        /// Updates the animation, true is returned if the animation has finished
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        protected abstract bool UpdateInternal(GameObject obj);

        public bool Update(GameObject obj)
        {
            if (firstUpdate)
            {
                if (Begining != null)
                    Begining(this, new EventArgs());
                firstUpdate = false;
            }

            bool val = UpdateInternal(obj);
            if (val)
                if (Completed != null)
                    Completed(this, new EventArgs());
            return val;
        }
           
	}
}
