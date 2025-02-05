using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    [SerializeField] private GameObject projectilePref;
    
    
    private bool _collidedLastFixedUpdate;
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.left * 5, Color.red);
        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit))
        {
            if (_collidedLastFixedUpdate) return;
            if (hit.transform.CompareTag("Destroyable"))
            {
                _collidedLastFixedUpdate = true;
                var rb = Instantiate(projectilePref, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

                rb.AddForce(-transform.right * 4, ForceMode.Impulse);
                
                Destroy(rb.gameObject, 4f);
                
                // hit.transform.GetComponent<SphereController>().Damage(1);
            }
        }
        else
        {
            _collidedLastFixedUpdate = false;
        }
    }
}
