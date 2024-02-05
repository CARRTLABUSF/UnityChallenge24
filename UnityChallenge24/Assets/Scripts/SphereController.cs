using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{   private int lives = 5; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ifHit()
    {
        
        lives--;

        
        if (lives == 0)
        {
            
            Destroy(gameObject);
        }
    }
}
