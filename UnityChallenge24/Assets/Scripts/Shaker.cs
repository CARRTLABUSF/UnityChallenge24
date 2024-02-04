using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float amount;
    Vector3 startPos; 
    Vector3 vibration;
    public bool colliding;
    // Start is called before the first frame update
    void Start()
    {   
        startPos = transform.position;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (colliding) shake();
        
    }

    void shake(){
        //have sphere randomly vibrate within unit circle
        vibration = Random.insideUnitSphere * amount;
        transform.position = startPos + vibration * Time.deltaTime;
    }
}
