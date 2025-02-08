using Interfaces;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    private Ray _ray;
    
    void Update()
    {
        PerformRaycast();
    }
    
    private void PerformRaycast()
    {
        _ray = new Ray(transform.position, transform.right * -1);
        
        if (Physics.Raycast(_ray, out var hit))
        {
            if (hit.collider.gameObject.CompareTag("Destroyable"))
            {
                MakeDamageTo(hit.collider.gameObject);
            }
        }
    }
    
    private void MakeDamageTo(GameObject obj)
    {
        if (obj.TryGetComponent(out IDestroyable destructible))
        {
            destructible.TakeDamage();
        }
    }
}
