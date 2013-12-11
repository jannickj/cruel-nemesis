using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Animations
{
	public class MoveTransformAnimation : TransformAnimation
	{
        private Vector3 cur;
        private Vector3 goal;

        private Func<Vector3> genFrom;
        private Func<Vector3> genTo;

        private Func<float> genSpeed; 

        public MoveTransformAnimation(Func<Vector3> genFrom, Func<Vector3> genTo, Func<float> genSpeed)
        {
            this.genFrom = genFrom;
            this.genTo = genTo;
            this.genSpeed = genSpeed;
        }

        public MoveTransformAnimation(Vector3 from, Vector3 to, float speed)
        {
            this.genFrom = () => from;
            this.genTo = () => to;
            this.genSpeed = () => speed;
        }

        protected override void  ResetInternal()
        {
            cur = genFrom();
            goal = genTo();
        }

        
        protected override bool UpdateInternal(GameObject obj)
        {
            var trans = obj.transform;
            var step = genSpeed() * Time.deltaTime;
            var newpos = Vector3.MoveTowards(cur, goal, step);
            trans.position = newpos;
            cur = newpos;

            //Debug.Log("cur: "+cur+" goal: "+goal+" newpos: "+newpos+" speed: "+genSpeed()+" step: "+step);

            return goal.Equals(cur);
        }
	}
}
