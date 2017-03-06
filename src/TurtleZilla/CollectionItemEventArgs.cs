using System;

namespace TurtleZilla
{
    [Serializable]
    internal class CollectionItemEventArgs<T> : EventArgs
    {
        public T Item { get; private set; }

        public CollectionItemEventArgs(T item)
        {
            if (item == null) throw new ArgumentException("item");
            Item = item;
        }

        public override string ToString()
        {
            return Item.ToString();
        }
    }
}
