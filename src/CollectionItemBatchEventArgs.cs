using System;
using System.Collections.Generic;

namespace TurtleZilla
{
    [Serializable]
    internal class CollectionItemBatchEventArgs<T> : EventArgs
    {
        public IEnumerable<T> Items { get; private set; }

        public CollectionItemBatchEventArgs(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentException("items");
            Items = items;
        }
    }
}
