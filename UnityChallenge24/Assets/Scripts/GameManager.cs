using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Check if the game is restarting
    public bool restarting = false;
    
    //Skipper Position Object
    public GameObject skipperPosition;
    //Skateboard Position Object
    public GameObject skateboardPosition;

    public GameObject skipperPrefab;
    public GameObject skateboardPrefab;

    //Parent object for all the hittable objects in the game
    public GameObject targets;

    void Start()
    {
        
        GameObject skipper = Instantiate(skipperPrefab, 
                                        skipperPosition.transform.position, 
                                        Quaternion.identity, 
                                        skipperPosition.transform);
        GameObject skateboard = Instantiate(skateboardPrefab, 
                                        skateboardPosition.transform.position, 
                                        Quaternion.identity, 
                                        skateboardPosition.transform);
                                 
    }

    void Update()
    {
        //Restart the game if all targets are "dead"
        if (targets.transform.childCount == 0){
            //If the game isn't currently restarting, restart it
            if (!restarting){
                StartCoroutine("Restart");
            }   
        }
    }

    //Game Restart Coroutine
    IEnumerator Restart(){
        restarting = true;
        Debug.Log("Restarting...");

        //Wait for 5 seconds before restarting the game
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
