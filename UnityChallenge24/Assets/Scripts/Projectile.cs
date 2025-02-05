using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out SphereController sphereController))
        {
            sphereController.Damage(1);
        }
        Destroy(gameObject);
    }
}
