using UnityEngine;

public class LaserHand : MonoBehaviour
{
    public float maxDistance = 100f;
    public LayerMask hitLayers;
    public LineRenderer laserLine;
    private bool canShoot = true;
    private float shootDelay = 0.5f;

    void Update()
    {
        RaycastHit hit;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + transform.forward * maxDistance;

        if (canShoot && Physics.Raycast(startPoint, transform.forward, out hit, maxDistance, hitLayers))
        {
            endPoint = hit.point;
            
            SphereController sphere = hit.collider.GetComponent<SphereController>();
            if (sphere != null)
            {
                sphere.TakeDamage();
                StartCoroutine(ShootDelay());
            }
        }

        if (laserLine != null)
        {
            laserLine.SetPosition(0, startPoint);
            laserLine.SetPosition(1, endPoint);
        }
    }

    private System.Collections.IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}