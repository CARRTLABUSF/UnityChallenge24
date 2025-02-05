using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skipperPosition;
    public GameObject skateboardPosition;

    [SerializeField] private GameObject SkipperPrefab;
    [SerializeField] private GameObject SkateboardPrefab;
    
    void Start()
    {
        Instantiate(SkipperPrefab, skipperPosition.transform);
        Instantiate(SkateboardPrefab, skateboardPosition.transform);
    }
    
}
