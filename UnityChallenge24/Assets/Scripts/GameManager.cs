using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skipperPosition;
    public GameObject skateboardPosition;
    
    [Header("Prefabs")]
    [SerializeField] GameObject skipperObj;
    [SerializeField] GameObject skateboardObj;

    void Start()
    {
        InstantiateObject(skipperObj, skipperPosition.transform);
        InstantiateObject(skateboardObj, skateboardPosition.transform);
    }

    //Instantiate new object that attach to a parent transform
    private void InstantiateObject(GameObject prefab, Transform parentTransform)
    {
        GameObject newObj = Instantiate(prefab, parentTransform);

        //set new transform attributes
        newObj.transform.position = parentTransform.position;
        newObj.transform.localScale = new Vector3(10, 10, 10);
        newObj.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
