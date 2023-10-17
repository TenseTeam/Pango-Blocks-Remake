namespace VUDK.Generic.Serializable
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public struct SerializableKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }

    [System.Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<SerializableKeyValuePair<TKey, TValue>> _dictionary;

        public TValue this[TKey key]
        {
            get
            {
                return Dict[key];
            }
            set
            {
                Dict[key] = value;
            }
        }

        public Dictionary<TKey, TValue> Dict => ToDictionary();

        public SerializableDictionary()
        {
            _dictionary = new List<SerializableKeyValuePair<TKey, TValue>>();
        }

        public SerializableDictionary(List<SerializableKeyValuePair<TKey, TValue>> keyPairValues) : this()
        {
            _dictionary = keyPairValues;
        }

        private Dictionary<TKey, TValue> ToDictionary()
        {
            var dict = new Dictionary<TKey, TValue>();

            foreach(var valuePair in _dictionary)
                dict[valuePair.Key] = valuePair.Value;

            return dict;
        }
    }
}