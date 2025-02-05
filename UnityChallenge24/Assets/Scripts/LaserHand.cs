using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.left * 5, Color.red);
        if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Destroyable"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
