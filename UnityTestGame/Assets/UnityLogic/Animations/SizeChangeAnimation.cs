using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnityLogic.Animations
{
	public class SizeChangeAnimation : GameObjectAnimation
	{
        private float pctPerUpdate;

        public SizeChangeAnimation(float pctPerUpdate)
        {
            this.pctPerUpdate = pctPerUpdate;
        }


        protected override void ResetInternal()
        {
        }

        protected override bool UpdateInternal(UnityEngine.GameObject obj)
        {
            var trans = obj.transform;
            var scale = trans.localScale;

            scale.x = scale.x * (1f + pctPerUpdate);
            scale.z = scale.z * (1f + pctPerUpdate);
            trans.localScale = scale;
            return false;
        }
    }
}
