using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    //Layer to be destroyed
    public LayerMask destroyLayer;
    //Numeric layer value
    private int destroyIndex;

    void Start(){
        destroyIndex = (int)Mathf.Log(destroyLayer.value, 2);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == destroyIndex){
            Destroy(other.gameObject);
        }
    }
}
