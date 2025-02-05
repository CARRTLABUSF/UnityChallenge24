using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab1; 
    public GameObject prefab2; 
    public Vector3 spawnPosition1; 
    public Vector3 spawnPosition2; 
    public Quaternion spawnRotation = Quaternion.identity; // Default rotation

    private bool hasInstantiated = false; // Prevents multiple instantiations

    void Start()
    {
        if (prefab1 == null)
        {
            //Debug.LogError("Prefab1 is NOT assigned in the Inspector");
            return;
        }
        
        if (prefab2 == null)
        {
            Debug.LogError("Prefab2 is NOT assigned in the Inspector");
            return;
        }

        Debug.Log("Prefabs assigned correctly: " + prefab1.name + " & " + prefab2.name);

        if (!hasInstantiated)
        {
            if (GameObject.Find(prefab1.name + "(Clone)") == null)
            {
                InstantiatePrefab(prefab1, spawnPosition1);
            }
            if (GameObject.Find(prefab2.name + "(Clone)") == null)
            {
                InstantiatePrefab(prefab2, spawnPosition2);
            }
            hasInstantiated = true;
        }
    }
    
    void InstantiatePrefab(GameObject prefab, Vector3 spawnPosition)
    {
        GameObject instantiatedObject = Instantiate(prefab, spawnPosition, spawnRotation);
        Debug.Log("Prefab instantiated at: " + spawnPosition);
    }
}
