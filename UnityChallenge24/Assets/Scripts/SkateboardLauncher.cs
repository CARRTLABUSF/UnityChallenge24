using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkateboardLauncher : MonoBehaviour
{
    [SerializeField] private StatsRef skateboardStats;
    [SerializeField] private GameObject skateboardProjectile;
    [SerializeField] private Transform launcherVisual;
    [SerializeField] private UnityEvent ready;
    [SerializeField] private bool isAuto;
    
    [Header("Stats")] 
    [SerializeField] private float initialDamage;
    [SerializeField] private float initialCooldown;


    private float _cooldownTimer;
    
    private void Awake()
    {
        skateboardStats.AddStat("damage", new Stat(initialDamage));
        skateboardStats.AddStat("cooldown", new Stat(initialCooldown));
    }

    public void UnlockAuto()
    {
        isAuto = true;
        Launch();
    }

    public void Launch()
    {
        if (_cooldownTimer > 0) return;
        var projectile = Instantiate(skateboardProjectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        
        projectile.Launch(transform.forward * 2, skateboardStats.GetStatValue("damage"));

        _cooldownTimer = skateboardStats.GetStatValue("cooldown");

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        float start = _cooldownTimer;
        while (_cooldownTimer > 0)
        {
            launcherVisual.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1 -  _cooldownTimer / start);
            _cooldownTimer -= Time.deltaTime;
            yield return null;
        }
        ready?.Invoke();
        if (isAuto)
        {
            //To prevent StackOverflow
            Invoke(nameof(Launch), 0);
        }
    }
}
