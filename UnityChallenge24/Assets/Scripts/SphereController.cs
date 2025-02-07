using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class SphereController : MonoBehaviour
{
    [SerializeField] private double lives;
    [SerializeField] private TextMeshProUGUI hpDisplay;

    private Animation _animation;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private ParticleSystem impactPS;
    private void Awake()
    {
        _animation = gameObject.GetComponent<Animation>();
        hpDisplay.text = lives.ToString("N");
    }

    public void Damage(double damage)
    {
        lives -= damage;
        _animation.Play();
        hpDisplay.text = lives.ToString("N");
        impactPS.Play();
        AudioManager.Instance.PlayOneShot(hitSound, 0.89f, 1.11f, 0.36f);
        
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.RestartGame();
        }
    }
}
