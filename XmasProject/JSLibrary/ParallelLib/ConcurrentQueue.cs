using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLibrary.ParallelLib
{
    public class ConcurrentQueue<T>
    {
        private Queue<T> queue = new Queue<T>();

        public T Dequeue()
        {
            lock (queue)
            {
                return this.queue.Dequeue();
            }
        }

        public void Enqueue(T val)
        {
            lock (queue)
            {
                queue.Enqueue(val);
            }
        }

        public bool TryDequeue(out T val)
        {
            lock (queue)
            {
                if (queue.Count > 0)
                {
                    val = queue.Dequeue();
                    return true;
                }
                else
                {
                    val = default(T);
                    return false;
                }
            }
        }

        public bool IsEmpty 
        {
            get
            {
                lock (queue)
                {
                    return queue.Count == 0;
                }
            }
        }
    }
}
