using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private UnityEvent paused;
    [SerializeField] private UnityEvent unPaused;

    private bool _isPaused;
    
    private void Awake()
    {
        //There is no need to retain the GameManager for this game
        Instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    public void SwitchPause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            unPaused?.Invoke();
        }
        else
        {
            Time.timeScale = 0;
            paused?.Invoke();
        }

        _isPaused = !_isPaused;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
