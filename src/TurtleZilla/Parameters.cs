using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleZilla
{
    [Serializable]
    public sealed class Parameters
    {
        private string _url;
        private string _product;
        private string _apiKey;

        public string Url
        {
            get { return _url ?? string.Empty; }
            set { _url = value; }
        }

        public string Product
        {
            get { return _product ?? string.Empty; }
            set { _product = value; }
        }
        
        public string APIKey
        {
            get { return _apiKey ?? string.Empty; }
            set { _apiKey = value; }
        }

        public override string ToString()
        {
            var list = new List<KeyValuePair<string, string>>();

            if (Url.Length > 0)
                list.Add(Pair("url", Url));

            if (Product.Length > 0)
                list.Add(Pair("product", Product));
            
            if (APIKey.Length > 0)
                list.Add(Pair("apikey", APIKey));

            return string.Join(";",
                Pairs(
                    Pair("url", Url),
                    Pair("product", Product),
                    Pair("apikey", APIKey))
                .Where(e => e.Value.Length > 0)
                .Select(e => e.Key + "=" + e.Value)
                .ToArray());
        }

        private KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
        
        private IEnumerable<KeyValuePair<string, string>> Pairs(params KeyValuePair<string, string>[] pairs)
        {
            return pairs;
        }
    }
}
