using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const float kRestartTime = 5f;
    public static GameManager Instance;

    private void Awake()
    {
        //There is no need to retain the GameManager for this game
        Instance = this;
    }
    
    private void Start()
    {
        
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameRoutine());
    }

    private static IEnumerator RestartGameRoutine()
    {
        Debug.LogWarning($"Restarting in {kRestartTime:####} seconds");
        yield return new WaitForSeconds(kRestartTime);
        Debug.LogWarning($"Restarting!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
