using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Skipper")]
    [SerializeField] private GameObject skipperSpawnPoint;
    [SerializeField] private GameObject skipperPrefab;
    [SerializeField] private Quaternion skipperRotation;
    [Header("Skateboard")]
    [SerializeField] private GameObject skateboardSpawnPoint;
    [SerializeField] private GameObject skateboardPrefab;
    [SerializeField] private Quaternion skateboardRotation;
    private void Start()
    {
        //Spawn skipper object and set its position w rotation 
        Instantiate(skipperPrefab, skipperSpawnPoint.transform.position, skipperRotation, skipperSpawnPoint.transform);
        //Spawn skateboard object and set its position w rotation 
        Instantiate(skateboardPrefab, skateboardSpawnPoint.transform.position, skateboardRotation, skateboardSpawnPoint.transform);
    }
}
