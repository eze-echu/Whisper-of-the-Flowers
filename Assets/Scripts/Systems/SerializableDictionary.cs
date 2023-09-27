using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> {
    public List<TKey> keys = new List<TKey>();
    public List<TValue> values = new List<TValue>();

    // You can add some methods here for convenience, like:
    public void Add(TKey key, TValue value) {
        keys.Add(key);
        values.Add(value);
    }

    public bool TryGetValue(TKey key, out TValue value) {
        int index = keys.IndexOf(key);
        if (index >= 0) {
            value = values[index];
            return true;
        } else {
            value = default(TValue);
            return false;
        }
    }

    // etc.
}