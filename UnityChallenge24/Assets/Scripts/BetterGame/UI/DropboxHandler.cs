using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DropboxHandler : MonoBehaviour
{
    private Dropdown dropDown;
    void Start(){
        dropDown = gameObject.ConvertTo<Dropdown>();
    }
    public void OnQuality(){
        QualitySettings.SetQualityLevel(dropDown.value);       
    }
    public void OnDifficulty(){
        BetterGameManager.difficulty = dropDown.value;
    }
}
