using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/StatsRef")]
public class StatsRef : ScriptableObject
{
    private readonly Dictionary<string, Stat> _stats = new();

    /// <summary>
    /// Add stat to this reference
    /// </summary>
    /// <param name="key"></param>
    /// <param name="stat"></param>
    public void AddStat(string key, in Stat stat)
    {
        _stats[key] = stat;
    }

    public void GetStat(string key, out Stat stat)
    {
        stat = _stats.GetValueOrDefault(key);
    }
    
    /// <summary>
    /// Returns the value stored in the stat
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public float GetStatValue(string key)
    {
        return _stats.GetValueOrDefault(key).Value;
    }
}
