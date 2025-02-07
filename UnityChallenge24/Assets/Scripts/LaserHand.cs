using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    [SerializeField] private GameObject projectilePref;
    [SerializeField] private StatsRef playerStats;
    
    private bool _collidedLastFixedUpdate;
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit))
        {
            if (_collidedLastFixedUpdate) return;
            if (hit.transform.CompareTag("Destroyable"))
            {
                _collidedLastFixedUpdate = true;

                for (int i = 0; i < (int)playerStats.GetStatValue("multishot"); i++)
                {
                    Invoke(nameof(Shoot), i * 0.1f);
                }
                
            }
        }
        else
        {
            _collidedLastFixedUpdate = false;
        }
    }

    private void Shoot()
    {
        var rb = Instantiate(projectilePref, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(-transform.right * 4, ForceMode.Impulse);
                
        //Destroy the projectile in 4 second if we somehow miss
        Destroy(rb.gameObject, 4f);
    }
}
