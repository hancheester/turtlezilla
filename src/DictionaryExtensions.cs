using System.Collections.Generic;

namespace TurtleZilla
{
    internal static class DictionaryExtensions
    {
        public static V Find<K, V>(this IDictionary<K, V> dict, K key)
        {
            return Find(dict, key, default(V));
        }

        public static V Find<K, V>(this IDictionary<K, V> dict, K key, V @default)
        {
            V value;
            return dict.TryGetValue(key, out value) ? value : @default;
        }

        public static V Pop<K, V>(this IDictionary<K, V> dict, K key)
        {
            var value = dict[key];
            dict.Remove(key);
            return value;
        }

        public static V TryPop<K, V>(this IDictionary<K, V> dict, K key)
        {
            return TryPop(dict, key, default(V));
        }

        public static V TryPop<K, V>(this IDictionary<K, V> dict, K key, V @default)
        {
            var value = dict.Find(key, @default);
            dict.Remove(key);
            return value;
        }
    }
}
