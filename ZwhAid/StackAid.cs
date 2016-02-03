using System.Collections.Generic;

namespace ZwhAid
{
    public class StackAid<T> where T : class,new()
    {
        protected Stack<T> statcks;
        public Stack<T> Statcks
        {
            set { statcks = value; }
            get { return statcks; }
        }

        private int capacity = 25;
        public int Capacity
        {
            set { capacity = value; }
            get { return capacity; }
        }

        public StackAid()
        {
            statcks = new Stack<T>();
        }

        public StackAid(int count)
        {
            capacity = count;
            statcks = new Stack<T>(Capacity);
        }

        public void Push(T t)
        {
            lock (statcks)
            {
                statcks.Push(t);
            }
        }

        public T Pop()
        {
            lock (statcks)
            {
                return statcks.Pop();
            }
        }

        public int Count
        {
            get { return statcks.Count; }
        }
    }
}
