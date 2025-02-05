using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class SphereController : MonoBehaviour
{
    [SerializeField] private long lives;

    private Animation _animation;

    private void Awake()
    {
        _animation = gameObject.GetComponent<Animation>();
    }

    public void Damage(int damage)
    {
        lives -= damage;
        _animation.Play();
        Debug.Log($"Damaged! Lives remaining: {lives}");
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.RestartGame();
        }
    }
}
