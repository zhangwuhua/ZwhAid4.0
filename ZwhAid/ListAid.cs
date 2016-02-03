using System.Collections.Generic;

namespace ZwhAid
{
    public class ListAid<T> where T : class,new()
    {
        protected List<T> lists;
        public List<T> Lists
        {
            set { lists = value; }
            get { return lists; }
        }

        private int capacity = 25;
        public int Capacity
        {
            set { capacity = value; }
            get { return capacity; }
        }

        public ListAid()
        {
            lists = new List<T>();
        }

        public ListAid(int count)
        {
            capacity = count;
            lists = new List<T>(Capacity);
        }

        public void Add(T t)
        {
            lock (lists)
            {
                lists.Add(t);
            }
        }

        public void Remove(T t)
        {
            lock (lists)
            {
                lists.Remove(t);
            }
        }

        public void Copy(ref T[] ts)
        {
            lock (lists)
            {
                ts = new T[Capacity];
                lists.CopyTo(ts);
            }
        }
    }
}
