using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Animations
{
	public abstract class GameGraphics
	{
        private Dictionary<GameObject, Queue<GameObjectAnimation>> animationQueues = new Dictionary<GameObject, Queue<GameObjectAnimation>>();
        private HashSet<GameObject> justAdded = new HashSet<GameObject>();
        public UnityFactory Factory { get; private set; }
        /// <summary>
        /// Auto deletes a gameobject when it has no more animations to execute
        /// </summary>
        public bool AutoDelete { get; protected set; }
        public bool IsFinished { get; protected set; }

        public GameGraphics()
        {
            AutoDelete = true;
        }

        public void Initialize(UnityFactory Factory)
        {
            this.Factory = Factory;
            Prepare();
        }

        protected abstract void Prepare();

        protected void OverwriteAnimation(GameObject gobj, GameObjectAnimation Animation)
        {
            if (!animationQueues.ContainsKey(gobj))
                justAdded.Add(gobj);
            Animation.Reset();
            Queue<GameObjectAnimation> anis = getQueue(gobj);
            anis.Clear();
            anis.Enqueue(Animation);
            
        }

        protected void EnqueueAnimation(GameObject gobj, GameObjectAnimation Animation)
        {
            if (!animationQueues.ContainsKey(gobj))
                justAdded.Add(gobj);

            Animation.Reset();
            Queue<GameObjectAnimation> anis = getQueue(gobj);
            anis.Enqueue(Animation);
            
        }


        private Queue<GameObjectAnimation> getQueue(GameObject gobj)
        {
            Queue<GameObjectAnimation> anis;
            if (!animationQueues.TryGetValue(gobj, out anis))
            {
                anis = new Queue<GameObjectAnimation>();
                this.animationQueues.Add(gobj, anis);
            }
            return anis;
        }


        public void Update()
        {
            foreach(var kv in animationQueues.ToArray())
            {
                GameObject gobj = kv.Key;
                Queue<GameObjectAnimation> anis = kv.Value;
                UpdateGobj(gobj, anis);
            }

            foreach (var gobj in justAdded.ToArray())
            {
                var anis = animationQueues[gobj];
                UpdateGobj(gobj, anis);
            }
        }


        private void UpdateGobj(GameObject gobj, Queue<GameObjectAnimation> anis)
        {
            GameObjectAnimation ani = anis.Peek();
            if (justAdded.Contains(gobj))
                justAdded.Remove(gobj);

            updateAni(ani, anis, gobj);

            if (this.animationQueues.Count == 0)
                this.IsFinished = true;
        }

        private void updateAni(GameObjectAnimation ani, Queue<GameObjectAnimation> anis, GameObject gobj)
        {
            bool isLast = anis.Count == 1;
            if (ani.Update(gobj))
            {
                if (!ani.AutoLooping)
                {
                    anis.Dequeue();
                    if (anis.Count == 0 && AutoDelete)
                    {
                        animationQueues.Remove(gobj);
                        GameObject.Destroy(gobj);
                    }
                }
            }
        }

        
	}
}
