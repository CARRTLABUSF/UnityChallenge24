using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private LineRenderer[] laserPointers;
    [SerializeField] private SphereController sphere;
    [SerializeField] private ParticleSystem laserImpactParticles;
    [SerializeField] private StatsRef spiderStats;

    private StatModifier _damageModifier;
    
    [Header("Stats")] 
    [SerializeField] private float initialDamage = 0.1f;

    private float _cooldown = 1.4f;
    private float _timer;
    
    private void Awake()
    {
        spiderStats.AddStat("laserCount", new Stat(0));
        _damageModifier = new StatModifier(1, StatModifier.Type.Multiplicative);
        Stat damageStat = new(initialDamage);
        damageStat.AddModifier(_damageModifier);
        spiderStats.AddStat("damage", damageStat);
        enabled = false;
    }

    private void Activate(int index)
    {
        Debug.Log(index);
        _cooldown -= 0.2f;
        LineRenderer laserPointer = laserPointers[index];
        laserPointer.gameObject.SetActive(true);
        
        Vector3 position = laserPointer.transform.position;
        Vector3 dir = (sphere.transform.position - position).normalized;
        Physics.Raycast(position, dir, out RaycastHit hit);
        laserPointer.SetPosition(0, position);
        laserPointer.SetPosition(1, hit.point);
        var ps = Instantiate(laserImpactParticles, hit.point, Quaternion.identity);
        ps.transform.LookAt(position);
        ps.transform.SetParent(laserPointer.transform);
        ps.transform.GetChild(0).position = hit.point + -dir * 0.1f;
    }

    public void StartHeatingUp()
    {
        StartCoroutine(HeatingUpRoutine());
    }

    /// <summary>
    /// Continuously increases laser damage   
    /// </summary>
    /// <returns></returns>
    private IEnumerator HeatingUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _damageModifier.Update(_damageModifier.Value * 2);
        }
    }
    
    public void ActivateNewLaser()
    {
        enabled = true;
        Activate((int)spiderStats.GetStatValue("laserCount"));
    }
    
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer += _cooldown;
            sphere.Damage(spiderStats.GetStatValue("damage") * spiderStats.GetStatValue("laserCount"));
        }
    }

    
}
