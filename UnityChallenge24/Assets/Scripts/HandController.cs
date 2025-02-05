using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField, Min(0)] private float amplitude = 1f;
    [SerializeField, Min(0)] private float frequency = 1f;
    [SerializeField] private float initialOffset; 
    
    private Vector3 _startPos;
    
    private void Start()
    {
        _startPos = transform.position;
        transform.position = new Vector3(_startPos.x, _startPos.y, _startPos.z + Mathf.Sin(initialOffset));
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Move the hand using the Sin function
    /// </summary>
    private void Move()
    {
        Vector3 newPos = _startPos;
        newPos.z += Mathf.Sin(Time.time * frequency + initialOffset) * amplitude;
        transform.position = newPos;
    }
}
