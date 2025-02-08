using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;  // Assign the prefab in the Inspector
    public Transform sphere;   // Assign the sphere in the Inspector

    void Start()
    {
        if (sphere == null)
        {
            Debug.LogError("Sphere reference is missing in GameManager!");
            return;
        }

        // Instantiate 2 prefabs on both sides of the sphere
        Instantiate(prefab, new Vector3(sphere.position.x - 2f, sphere.position.y, sphere.position.z), Quaternion.identity);
        Instantiate(prefab, new Vector3(sphere.position.x + 2f, sphere.position.y, sphere.position.z), Quaternion.identity);
    }
}
