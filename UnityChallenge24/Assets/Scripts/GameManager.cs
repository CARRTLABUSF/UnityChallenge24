using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject skipperPosition;
    public GameObject skateboardPosition;

    void Start()
    {
        GameObject skipperPrefab = Resources.Load<GameObject>("SkipperPrefab");
        GameObject skateboardPrefab = Resources.Load<GameObject>("SkateboardPrefab");

        if (skipperPrefab && skipperPosition)
        {
            // Instantiate skipperPrefab and set skipperPosition as its parent
            GameObject skipper = Instantiate(skipperPrefab, skipperPosition.transform.position, Quaternion.identity, skipperPosition.transform);
            skipper.transform.localPosition = Vector3.zero; // adjust local position
        }

        if (skateboardPrefab && skateboardPosition)
        {
            // Instantiate skateboardPrefab and set SkateboardPos as its parent
            GameObject skateboard = Instantiate(skateboardPrefab, skateboardPosition.transform.position, Quaternion.identity, skateboardPosition.transform);
            skateboard.transform.localPosition = Vector3.zero;
        }
    }
}
