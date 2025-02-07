using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public long Coins => _coins;
    
    private long _coins;

    [SerializeField] private TextMeshProUGUI coinsLabel;

    [SerializeField] private StatsRef playerStats;

    [Header("Stats")] 
    [SerializeField] private float initialFrequency = 1;

    [SerializeField] private float initialProjectileDamage = 1;
    [SerializeField] private float initialMultishot = 1;
    private void Awake()
    {
        Instance = this;
        //Initialize stats
        playerStats.AddStat("frequency", new Stat(initialFrequency));
        playerStats.AddStat("projectile_damage", new Stat(initialProjectileDamage));
        playerStats.AddStat("multishot", new Stat(initialMultishot));
        
    }

    public void AddCoin()
    {
        _coins += 1 * 100;
        coinsLabel.text = _coins.ToString();
    }

    public void Pay(long cost)
    {
        if (cost > _coins)
        {
            throw new ArgumentException("Price is larger than coins amount!");
        }
        _coins -= cost;
    }
}
