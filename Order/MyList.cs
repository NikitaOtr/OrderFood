using System.Collections;
using System.Collections.Generic;

namespace Order
{
    class MyList<T> : ICollection<T>, IList<T> where T : OrderItem
    {
        private List<T> list;

        public MyList()
        {
            this.list = new List<T>();
        }

        # region implementation of interfaces

        public T this[int index] {
            get => list[index]; 
            set => list[index] = value; 
        }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        public int CalculateTotalSum()
        {
            int totalSum = 0;
            foreach (T orderItem in list)
            {
                totalSum += orderItem.TotalPrice;
            }
            return totalSum;
        }

    }
}
