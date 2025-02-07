using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour, IHittable
{
    //The number of lives
    public int lives = 5;
    //Hit cooldown to prevent hit registration every frame
    public bool hitCooldown = false;

    void Update()
    {
        if (lives <= 0){
            Debug.Log("Destroyed: " + transform.name);
            Destroy(transform.gameObject);
        }
    }

    public void OnHit(){
        if (!hitCooldown){
            StartCoroutine("TakeDamage");
        }
    }

    //Damage Coroutine
    IEnumerator TakeDamage(){
        hitCooldown = true;
        lives -= 1;
        Debug.Log("Lives: " + lives);

        //Wait for 0.5 seconds and release the hit cooldown
        yield return new WaitForSeconds(0.5f);
        hitCooldown = false;
    }

}
