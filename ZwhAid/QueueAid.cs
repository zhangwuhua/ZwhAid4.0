using System.Collections.Generic;

namespace ZwhAid
{
    public class QueueAid<T> where T : class,new()
    {
        protected Queue<T> queues;
        public Queue<T> Queues
        {
            set { queues = value; }
            get { return queues; }
        }

        private int capacity = 25;
        public int Capacity
        {
            set { capacity = value; }
            get { return capacity; }
        }

        public QueueAid() { }

        public QueueAid(T t)
        {
            queues = new Queue<T>();
        }

        public QueueAid(T t, int count)
        {
            capacity = count;
            queues = new Queue<T>(Capacity);
        }

        public void Enqueue(T t)
        {
            lock (queues)
            {
                queues.Enqueue(t);
            }
        }

        public T Dequeue()
        {
            lock (queues)
            {
                return queues.Dequeue();
            }
        }

        public int Count
        {
            get { return queues.Count; }
        }
    }
}