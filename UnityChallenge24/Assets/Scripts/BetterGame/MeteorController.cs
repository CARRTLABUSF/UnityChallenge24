using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorController : MonoBehaviour, IHittable
{
    //Number of lives
    public int lives = 1;
    //Current position vector
    private Vector3 currentPosition;
    //Current rotation vector
    private Vector3 rotation;
    //Movement speed
    public float moveSpeed = 5.0f;
    //Rotation speed
    public float rotationSpeed = 150.0f;

    void Start(){
        moveSpeed *= (UnityEngine.Random.value * 0.5f) + 1.0f;
        moveSpeed += BetterGameManager.difficultyModifier * BetterGameManager.difficulty;
        currentPosition = transform.position;
        rotation = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    void Update(){
        if (lives <= 0){
            BetterGameManager.gameScore += BetterGameManager.meteorScore;
            Destroy(transform.gameObject);
        }
        Fly();
    }

    void Fly(){
        currentPosition.z -= moveSpeed * Time.deltaTime;
        transform.position = currentPosition;
        transform.Rotate(rotation.x * rotationSpeed * Time.deltaTime, 
                        rotation.y * rotationSpeed * Time.deltaTime, 
                        rotation.z * rotationSpeed * Time.deltaTime);
    }

    public void OnHit(){
        lives -= 1;
    }
}
