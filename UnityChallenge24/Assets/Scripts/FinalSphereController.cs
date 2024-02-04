using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalSphereController : MonoBehaviour
{
    public int HP;
    
    [SerializeField] GameObject winscreen;
    
    void Update()
    {
        if (HP == 0) {
            gameObject.SetActive(false);
            winscreen.GetComponent<RawImage>().color = new Color(100,100,100, 100);
        }
    }
}
