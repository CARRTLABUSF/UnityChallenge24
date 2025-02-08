using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private float _damage;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 force, float damage)
    {
        rb.AddForce(force, ForceMode.Impulse);
        _damage = damage;
        
        //Destroy the projectile in 10 second if we somehow miss
        Destroy(gameObject, 10f);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out SphereController sphereController))
        {
            sphereController.Damage(_damage);
        }
        Destroy(gameObject);
    }
}
