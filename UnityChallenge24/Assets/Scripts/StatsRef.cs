using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/StatsRef")]
public class StatsRef : ScriptableObject
{
    private readonly Dictionary<string, Stat> _stats = new();

    public void AddStat(string key, in Stat stat)
    {
        _stats.Add(key, stat);
    }

    public void GetStat(string key, out Stat stat)
    {
        stat = _stats.GetValueOrDefault(key);
    }
    
    public float GetStatValue(string key)
    {
        return _stats.GetValueOrDefault(key).Value;
    }
}
