using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skipperPosition;
    public GameObject skateboardPosition;
    public GameObject skipperPrefab;
    public GameObject skateboardPrefab;
    // Start is called before the first frame update
    void Start()
    {   //instatiate prefab models to their positions
        var skipperClone = Instantiate(skipperPrefab, skipperPosition.gameObject.transform.position , Quaternion.identity);
        var skateClone = Instantiate(skateboardPrefab, skateboardPosition.gameObject.transform.position , Quaternion.identity);

        //set prefab model as childern of the position gameobject
        skipperClone.transform.SetParent(skipperPosition.transform);
        skateClone.transform.SetParent(skateboardPosition.transform);
    }

}
