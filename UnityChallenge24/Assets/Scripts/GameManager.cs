using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const float kRestartTime = 5f;
    public static GameManager Instance;
    
    [Header("Skipper")]
    [SerializeField] private GameObject skipperSpawnPoint;
    [SerializeField] private GameObject skipperPrefab;
    [SerializeField] private Quaternion skipperRotation;
    [Header("Skateboard")]
    [SerializeField] private GameObject skateboardSpawnPoint;
    [SerializeField] private GameObject skateboardPrefab;
    [SerializeField] private Quaternion skateboardRotation;

    private void Awake()
    {
        //There is no need to retain the GameManager for this game
        Instance = this;
    }
    
    private void Start()
    {
        //Spawn skipper object and set its position w rotation 
        Instantiate(skipperPrefab, 
                    skipperSpawnPoint.transform.position, 
                    skipperRotation, 
                    skipperSpawnPoint.transform);
        
        //Spawn skateboard object and set its position w rotation 
        Instantiate(skateboardPrefab, 
                    skateboardSpawnPoint.transform.position, 
                    skateboardRotation, 
                    skateboardSpawnPoint.transform);
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
