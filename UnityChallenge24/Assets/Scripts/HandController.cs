using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandController : MonoBehaviour
{
    [SerializeField] private AudioClip coinsCollectSound;
    [SerializeField, Min(0)] private float amplitude = 1f;
    [SerializeField] private float initialOffset;

    [SerializeField]
    private StatsRef playerStats;
    private Vector3 _startPos;

    private StatModifier _baseFrequencyModifier;
    
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
        newPos.z += Mathf.Sin(Time.time * playerStats.GetStatValue("frequency") + initialOffset) * amplitude;
        transform.position = newPos;
    }

    private void OnParticleCollision(GameObject other)
    {
        AudioManager.Instance.PlayOneShot(coinsCollectSound, 0.88f, 1.12f, 0.1f);
        Player.Instance.AddCoin();
    }
}
