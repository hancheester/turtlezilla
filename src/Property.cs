using System;

namespace TurtleZilla
{
    internal interface IProperty
    {
        Type ObjectType { get; }
        Type PropertyType { get; }
        bool ReadOnly { get; }
        object GetValue(object obj);
        void SetValue(object obj, object value);
    }

    internal interface IProperty<T> : IProperty
    {
        object GetValue(T obj);
        void SetValue(T obj, object value);
    }

    internal interface IProperty<T, PT> : IProperty<T>
    {
        new PT GetValue(T obj);
        void SetValue(T obj, PT value);
    }

    internal sealed class Property<T, PT> : IProperty<T, PT>
    {
        private readonly Func<T, PT> _getter;
        private readonly Action<T, PT> _setter;

        public Type ObjectType { get { return typeof(T); } }
        public Type PropertyType { get { return typeof(PT); } }
        public bool ReadOnly { get { return _setter == null; } }

        public Property(Func<T, PT> getter)
            : this(getter, null) {}

        public Property(Func<T, PT> getter, Action<T, PT> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public PT GetValue(T obj) { return _getter(obj); }
        public void SetValue(T obj, PT value)
        {
            if (_setter == null) throw new InvalidOperationException("Cannot write to a read-only property.");
            _setter(obj, value);
        }

        object IProperty<T>.GetValue(T obj) { return GetValue(obj); }
        void IProperty<T>.SetValue(T obj, object value) { SetValue(obj, (PT)value); }
        object IProperty.GetValue(object obj) { return GetValue((T)obj); }
        void IProperty.SetValue(object obj, object value) { SetValue((T)obj, (PT)value); }
    }
}
