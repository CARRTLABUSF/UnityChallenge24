using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private StatsRef playerStats;
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out SphereController sphereController))
        {
            sphereController.Damage(playerStats.GetStatValue("projectile_damage"));
        }
        Destroy(gameObject);
    }
}
