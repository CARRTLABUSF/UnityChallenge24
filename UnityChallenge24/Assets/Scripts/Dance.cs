using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startPos;
    public float speed;
    public float range;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time * speed) * range, startPos.z);
    }
}
