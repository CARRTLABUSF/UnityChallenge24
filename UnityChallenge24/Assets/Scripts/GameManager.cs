using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skipperPosition;
    public GameObject skateboardPosition;

    void Start()
    {
        
        GameObject skipperPrefab = Resources.Load<GameObject>("SkipperPrefab");
        if (skipperPrefab != null)
        {
            GameObject skipperInstance = Instantiate(skipperPrefab, new Vector3(-1.44000006f,0.699999988f,-0.949999988f), Quaternion.identity);
            
          
        }

        
        GameObject skateboardPrefab = Resources.Load<GameObject>("SkateboardPrefab");
        if (skateboardPrefab != null)
        {
            GameObject skateboardInstance = Instantiate(skateboardPrefab, new Vector3(-1.16900003f,1.32000005f,0.930000007f), Quaternion.identity);
            
        }
    }

    void Update()
    {
        
    }
}
