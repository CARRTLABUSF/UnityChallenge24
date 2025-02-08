using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameObject skipperPosition;
    public GameObject skateboardPosition;

    [SerializeField] private GameObject SkipperPrefab;
    [SerializeField] private GameObject SkateboardPrefab;
    
    // Initializing Singleton 
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void Start()
    {
        Instantiate(SkipperPrefab, skipperPosition.transform);
        Instantiate(SkateboardPrefab, skateboardPosition.transform);
    }

    public void Restart()
    {
        StartCoroutine(RestartScene());
    }
    
    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    
}
