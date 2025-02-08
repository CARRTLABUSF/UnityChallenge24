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
    [SerializeField] private float initialGainPerParticle = 1;
    [SerializeField] private float initialMinParticlesPerHit = 1;
    [SerializeField] private float initialMaxParticlesPerHit = 1;
    private void Awake()
    {
        Instance = this;
        //Initialize stats
        playerStats.AddStat("frequency", new Stat(initialFrequency));
        playerStats.AddStat("projectile_damage", new Stat(initialProjectileDamage));
        playerStats.AddStat("multishot", new Stat(initialMultishot));
        playerStats.AddStat("gainPerParticle", new Stat(initialGainPerParticle));
        playerStats.AddStat("minParticlesPerHit", new Stat(initialMinParticlesPerHit));
        playerStats.AddStat("maxParticlesPerHit", new Stat(initialMaxParticlesPerHit));
    }

    public void AddCoin()
    {
        _coins = Math.Min(_coins + (long)(1 * playerStats.GetStatValue("gainPerParticle")), long.MaxValue - 10);
        coinsLabel.text = _coins.ToString();
    }

    public void Pay(long cost)
    {
        if (cost > _coins)
        {
            throw new ArgumentException("Price is larger than coins amount!");
        }
        _coins -= cost;
        coinsLabel.text = _coins.ToString();
    }
}
