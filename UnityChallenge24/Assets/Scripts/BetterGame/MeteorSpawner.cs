using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{   
    //Meteor prefab
    public GameObject meteorPrefab;
    //Spawn cooldown (delay between spawns)
    private bool spawnCoolDown = false;
    //Special time modifier
    public float timeModifier = 5.0f;

    void Update()
    {
        if (!spawnCoolDown){
            StartCoroutine("Spawn");
        }
    }

    IEnumerator Spawn(){
        spawnCoolDown = true;
        yield return new WaitForSeconds((5.0f + Random.value * timeModifier) / (1f + BetterGameManager.difficulty));
        Instantiate(meteorPrefab, transform.position, Quaternion.identity, transform);
        spawnCoolDown = false;
    }
    
}
