using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarshipController : MonoBehaviour
{    
    //Movement speed
    public float moveSpeed = 5.0f;
    //Character controller for the starship
    public CharacterController controller; 

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput(){
        float x = Input.GetAxis("Horizontal");
        Vector3 moveDirection = transform.right * x;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        Die();
    }

    void Die(){
        Destroy(transform.gameObject);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Summary");
    }
}
