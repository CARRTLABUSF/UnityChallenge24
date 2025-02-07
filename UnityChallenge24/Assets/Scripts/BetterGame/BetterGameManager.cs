using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetterGameManager : MonoBehaviour
{
    //Level distance in one direction from the starship
    public float levelRange = 2.5f;
    //Starship prefab
    public GameObject starship;
    //Level bounds prefab
    public GameObject levelBoundary;
    //Level bounds container
    public GameObject boundsContainer;
    //Left boundary x coordinate
    private float leftBoundary;
    //Right boundary x coordinate
    private float rightBoundary;
    //Difficulty level: 0 - Easy, 1 - Normal, 2 - Hard
    public static int difficulty = 0;
    //Special difficulty modifier
    public static float difficultyModifier = 1.5f;
    //Game score
    public static float gameScore;
    //Score from destroying a meteor
    public static float meteorScore;
    //Score per second of lifetime
    public static float timeScore;
    //Text object to display current score
    public Text scoreBoard;

    void Start()
    {
        gameScore = 0.0f;
        meteorScore = 5.0f;
        timeScore = 1.0f;

        StartCoroutine("IncreaseTimeScore");

        leftBoundary = starship.transform.position.x - levelRange;
        rightBoundary = starship.transform.position.x + levelRange;
        Instantiate(levelBoundary, new Vector3(leftBoundary, 0, 0), Quaternion.identity, boundsContainer.transform);
        Instantiate(levelBoundary, new Vector3(rightBoundary, 0, 0), Quaternion.identity, boundsContainer.transform);
    }

    void Update()
    {
        scoreBoard.text = "Score: " + (int)gameScore;
    }

    IEnumerator IncreaseTimeScore(){
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            gameScore += 1.0f;
        }
    }
}
