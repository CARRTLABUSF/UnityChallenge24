using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
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

    private void OnParticleCollision(GameObject other)
    {
        //Randomize pitch for each coin
        audioSource.pitch = 1 + (Random.value * 2 - 1) * 0.12f; 
        audioSource.PlayOneShot(audioSource.clip);
        Player.Instance.AddCoin();
    }
}
