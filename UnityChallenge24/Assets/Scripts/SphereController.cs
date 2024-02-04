using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public int HP;
    
    
    void Update()
    {
        if (HP == 0) Destroy(gameObject);
    }
}
