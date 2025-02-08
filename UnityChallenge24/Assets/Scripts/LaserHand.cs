using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    [SerializeField] private GameObject projectilePref;
    [SerializeField] private StatsRef playerStats;
    [SerializeField] private AudioClip shootSound;
    
    private bool _collidedLastFixedUpdate;
    private void FixedUpdate()
    {
        //Raycast until find the sphere
        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit))
        {
            if (_collidedLastFixedUpdate) return;
            if (hit.transform.CompareTag("Destroyable"))
            {
                _collidedLastFixedUpdate = true;

                for (int i = 0; i < (int)playerStats.GetStatValue("multishot"); i++)
                {
                    Invoke(nameof(Shoot), i * 0.05f);
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
        AudioManager.Instance.PlayOneShot(shootSound, 0.87f,1.1f, 0.3f);
        var projectile = Instantiate(projectilePref, transform.position, Quaternion.identity).GetComponent<Projectile>();

        projectile.Launch(-transform.right * 4, playerStats.GetStatValue("projectile_damage"));
    }
}
