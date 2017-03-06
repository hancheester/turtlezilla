using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace TurtleZilla
{
    [Serializable]
    internal class ListViewItem<T> : ListViewItem
    {
        public ListViewItem() { }
        public ListViewItem(string text) : base(text) { }
        public ListViewItem(string text, int imageIndex) : base(text, imageIndex) { }
        public ListViewItem(string[] items) : base(items) { }
        public ListViewItem(string[] items, int imageIndex) : base(items, imageIndex) { }
        public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font) : base(items, imageIndex, foreColor, backColor, font) { }
        public ListViewItem(ListViewSubItem[] subItems, int imageIndex) : base(subItems, imageIndex) { }
        public ListViewItem(ListViewGroup group) : base(group) { }
        public ListViewItem(string text, ListViewGroup group) : base(text, group) { }
        public ListViewItem(string text, int imageIndex, ListViewGroup group) : base(text, imageIndex, group) { }
        public ListViewItem(string[] items, ListViewGroup group) : base(items, group) { }
        public ListViewItem(string[] items, int imageIndex, ListViewGroup group) : base(items, imageIndex, group) { }
        public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup group) : base(items, imageIndex, foreColor, backColor, font, group) { }
        public ListViewItem(ListViewSubItem[] subItems, int imageIndex, ListViewGroup group) : base(subItems, imageIndex, group) { }
        public ListViewItem(string text, string imageKey) : base(text, imageKey) { }
        public ListViewItem(string[] items, string imageKey) : base(items, imageKey) { }
        public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font) : base(items, imageKey, foreColor, backColor, font) { }
        public ListViewItem(ListViewSubItem[] subItems, string imageKey) : base(subItems, imageKey) { }
        public ListViewItem(string text, string imageKey, ListViewGroup group) : base(text, imageKey, group) { }
        public ListViewItem(string[] items, string imageKey, ListViewGroup group) : base(items, imageKey, group) { }
        public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font, ListViewGroup group) : base(items, imageKey, foreColor, backColor, font, group) { }
        public ListViewItem(ListViewSubItem[] subItems, string imageKey, ListViewGroup group) : base(subItems, imageKey, group) { }
        protected ListViewItem(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public new T Tag
        {
            get { return (T)base.Tag; }
            set { base.Tag = value; }
        }
    }
}
