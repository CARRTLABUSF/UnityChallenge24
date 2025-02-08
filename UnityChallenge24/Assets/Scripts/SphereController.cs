using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
    [SerializeField] private double lives;
    [SerializeField] private TextMeshProUGUI hpDisplay;
    
    private Animation _animation;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private ParticleSystem impactPS;
    [SerializeField] private StatsRef playerStats;

    [SerializeField]
    private float growthFactor, shrinkSpeed; // How much size increases per damage

    private Vector3 initialScale;
    private Vector3 targetScale;
    private Vector3 velocity = Vector3.zero; // For SmoothDamp
    private void Awake()
    {
        hpDisplay.text = lives.ToString("N");
        initialScale = transform.localScale;
        targetScale = initialScale;
    }

    private void Start()
    {
        UpdateImpactPS();
    }

    public void UpdateImpactPS()
    {
        var em = impactPS.emission;
        var burst = em.GetBurst(0);
        burst.minCount = (short)playerStats.GetStatValue("minParticlesPerHit");
        burst.maxCount = (short)playerStats.GetStatValue("maxParticlesPerHit");
        em.SetBurst(0, burst);
    }
    
    private void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, 
            shrinkSpeed * Time.deltaTime);
    }

    public void Damage(double damage)
    {
        transform.localScale += initialScale * growthFactor;
        lives -= damage;
        hpDisplay.text = lives.ToString("N");
        impactPS.Play();
        AudioManager.Instance.PlayOneShot(hitSound, 0.89f, 1.11f, 0.36f);
        
        if (lives <= 0)
        {
            // gameObject.SetActive(false);
            hpDisplay.gameObject.SetActive(false);
            gameObject.GetComponent<Animation>().Play();
        }
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
