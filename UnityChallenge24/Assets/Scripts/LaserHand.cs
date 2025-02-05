using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
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
                hit.transform.GetComponent<SphereController>().Damage(1);
            }
        }
        else
        {
            _collidedLastFixedUpdate = false;
        }
    }
}
