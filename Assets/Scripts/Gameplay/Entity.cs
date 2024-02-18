using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private readonly HashSet<string> _tags = new();
    private readonly Dictionary<string, object> _values = new();

    public T GetValue<T>(string key) where T : class
    {
        return _values[key] as T;
    }

    public bool HasTag(string tag)
    {
        return _tags.Contains(tag);
    }

    public bool TryGetValue<T>(string key, out T result) where T : class
    {
        bool hasValue = _values.TryGetValue(key, out object value);
        result = value as T;
        return hasValue;
    }

    public void AddValue(string key, object value)
    {
        _values.Add(key, value);
    }

    public void RemoveValue(string key)
    {
        _values.Remove(key);
    }
}