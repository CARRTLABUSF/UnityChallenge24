using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skipperPosition;
    public GameObject skateboardPosition;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate the skipper at the skipperPosition from resources
        GameObject skipper = Instantiate(Resources.Load("SkipperPrefab"), skipperPosition.transform.position, Quaternion.identity) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
