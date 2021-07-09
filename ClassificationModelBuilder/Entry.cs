using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificationModelBuilder
{
    public class Entry<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public Entry(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }
}
