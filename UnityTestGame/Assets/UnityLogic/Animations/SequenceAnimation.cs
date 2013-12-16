using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Animations
{
	public class SequenceAnimation : GameObjectAnimation
	{
        private int curAni = 0;
        private List<GameObjectAnimation> anis = new List<GameObjectAnimation>();

        public void AddAnimation(GameObjectAnimation ani)
        {
            anis.Add(ani);
        }

        protected override void ResetInternal()
        {
            curAni = 0;
        }

        protected override bool UpdateInternal(GameObject obj)
        {
            if (anis.Count == 0)
                return true;
            

            var ani = anis[curAni];
            if(ani.Update(obj))
                if (!ani.AutoLooping)
                {
                    curAni++;
                }

            if (anis.Count < curAni)
                return true;
            return false;
        }
    }
}
