﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Support
{
    namespace Types
    {
        public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
        {
            [SerializeField]
            TKey[] m_keys;
            [SerializeField]
            TValue[] m_values;

            public SerializableDictionary() : base() { }
            public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict) { }
            public SerializableDictionary(IDictionary<TKey, TValue> dict, IEqualityComparer<TKey> comparer) : base(dict, comparer) { }
            public SerializableDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
            public SerializableDictionary(Int32 capacity) : base(capacity) { }
            public SerializableDictionary(Int32 capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }

            public void CopyFrom(IDictionary<TKey, TValue> dict)
            {
                this.Clear();
                foreach (var kvp in dict)
                {
                    this[kvp.Key] = kvp.Value;
                }
            }

            public void OnAfterDeserialize()
            {
                if (m_keys != null && m_values != null && m_keys.Length == m_values.Length)
                {
                    this.Clear();
                    int n = m_keys.Length;
                    for (int i = 0; i < n; ++i)
                    {
                        this[m_keys[i]] = m_values[i];
                    }

                    m_keys = null;
                    m_values = null;
                }

            }

            public void OnBeforeSerialize()
            {
                int n = this.Count;
                m_keys = new TKey[n];
                m_values = new TValue[n];

                int i = 0;
                foreach (var kvp in this)
                {
                    m_keys[i] = kvp.Key;
                    m_values[i] = kvp.Value;
                    ++i;
                }
            }
        }
    }
}