using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /* public GameObject skipperPosition;
     public GameObject skateboardPosition;*/
    public GameObject[] spawnObjects;  // Array of objects to spawn
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;
    public float spawnRadiusMin = 0.5f;  // Minimum spawn distance
    public float spawnRadiusMax = 2.0f;  // Maximum spawn distance
    public int maxObjects = 3;  // Maximum objects to spawn at once
    public float destroyTime = 5f;

    private List<GameObject> activeObjects = new List<GameObject>();

    // Center point (Modify this based on your needs)
    private Vector3 spawnCenter = new Vector3(0f, 1.17f, -3.57f);

    // Game progression variables
    private int destroyedObjectsCount = 0;
    private float spawnIntervalDecrement = 0.1f;  // Time to reduce spawn interval by
    private int maxObjectsCap = 10;  // Maximum number of objects allowed at once

  /*  public Text scoreText;
    public int score;
*/

    private bool isPaused = false; // Track if the game is paused

    public GameObject pauseMenu;  // Reference to the pause menu UI
    public Button resumeButton;   // Reference to the resume button
    public Button quitButton;     // Reference to the quit button
    public Button playButton;     // Reference to the play button (start screen)
    void Start()
    {
        StartCoroutine(SpawnObjects());
        // Initially show the play button and hide the pause menu
        pauseMenu.SetActive(false);

        // Button event listeners
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        playButton.onClick.AddListener(StartGame);
    }
    void Update()
    {
        // Toggle pause with the ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            if (activeObjects.Count < maxObjects)
            {
                SpawnRandomObject();
            }

            // Gradually increase spawn rate and max objects
            AdjustGameDifficulty();
        }
    }

    void SpawnRandomObject()
    {
        if (spawnObjects.Length == 0) return;

        // Random angle for spawning in a circular area
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Random radius within min/max range
        float radius = Random.Range(spawnRadiusMin, spawnRadiusMax);

        // Compute world-space position around the center point
        Vector3 spawnPosition = spawnCenter + new Vector3(
            Mathf.Cos(angle) * radius,   // X position
            Random.Range(-0.5f, 0.5f),   // Slight randomization in Y
            Mathf.Sin(angle) * radius    // Z position (keep around center)
        );

        // Choose a random object
        GameObject randomObject = spawnObjects[Random.Range(0, spawnObjects.Length)];

        // Spawn the object
        GameObject spawned = Instantiate(randomObject, spawnPosition, Quaternion.identity);
        activeObjects.Add(spawned);

        // Start the destroy coroutine to clean up objects
        StartCoroutine(DestroyAfterTime(spawned, destroyTime));

        // Apply continuous rotation on Y-axis
        StartCoroutine(RotateObjectContinuously(spawned));
    }

    // Coroutine to continuously rotate an object around the Y-axis
    IEnumerator RotateObjectContinuously(GameObject spawnedObject)
    {
        while (spawnedObject != null)
        {
            spawnedObject.transform.Rotate(0, 50 * Time.deltaTime, 0); // Rotate 50 degrees per second
            yield return null; // Wait until the next frame
        }
    }

    IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        if (activeObjects.Contains(obj))
        {
            activeObjects.Remove(obj);
            Destroy(obj);

            // Increment destroyed objects counter
            destroyedObjectsCount++;

            // After an object is destroyed, check if we should increase difficulty
            AdjustGameDifficulty();

            
        }
    }

    void AdjustGameDifficulty()
    {
        // Increase spawn rate (decrease interval)
        spawnIntervalMin = Mathf.Max(0.2f, spawnIntervalMin - spawnIntervalDecrement);  // Prevent going below a cap
        spawnIntervalMax = Mathf.Max(0.5f, spawnIntervalMax - spawnIntervalDecrement);

        // Gradually increase the max number of objects that can spawn at once
        if (maxObjects < maxObjectsCap)
        {
            maxObjects = Mathf.Min(maxObjectsCap, maxObjects + 1);  // Cap the max objects at 10
        }
    }

    /*public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            score++;
            Debug.Log(score);
            //scoreText.text = "Score: " + destroyedObjectsCount.ToString();
            scoreText.text = "Score: " + score.ToString() ;
        }
    }*/


    // Method to start the game
    void StartGame()
    {
        Time.timeScale = 1;  // Unpause if the game is starting
        playButton.gameObject.SetActive(false);  // Hide the play button after the game starts
    }

    // Method to pause the game
    void PauseGame()
    {
        Time.timeScale = 0;  // Freeze the game time (pauses the game)
        isPaused = true;     // Set the paused state to true
        pauseMenu.SetActive(true); // Show the pause menu
    }
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops the game in the editor
#else
        Application.Quit();  // Exit the game when in a built version
#endif
    }
    // Method to resume the game
    void ResumeGame()
    {
        Time.timeScale = 1;  // Unfreeze the game time (resumes the game)
        isPaused = false;    // Set the paused state to false
        pauseMenu.SetActive(false); // Hide the pause menu
    }

}
