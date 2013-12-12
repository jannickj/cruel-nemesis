using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Animations
{
	public class ParallelAnimation : GameObjectAnimation
	{
        private Dictionary<GameObjectAnimation, bool> animations = new Dictionary<GameObjectAnimation, bool>();
        private HashSet<GameObjectAnimation> needsCompletion = new HashSet<GameObjectAnimation>();

        protected override void ResetInternal()
        {
            needsCompletion.Clear();
            foreach (var kv in animations)
            {
                if (kv.Value)
                    needsCompletion.Add(kv.Key);
                kv.Key.Reset();
            }
        }

        protected override bool UpdateInternal(GameObject obj)
        {
            foreach (var kv in animations)
            {
                var val = kv.Key.Update(obj);
                if (val)
                {
                    if (needsCompletion.Contains(kv.Key))
                        needsCompletion.Remove(kv.Key);

                    if (needsCompletion.Count == 0)
                        return true;
                   

                }
            }
            return false;
        }

        public void Add(GameObjectAnimation ani, bool MustFinish)
        {
            this.animations.Add(ani, MustFinish);
        }
    }
}
