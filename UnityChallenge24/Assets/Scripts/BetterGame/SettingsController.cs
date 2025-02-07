using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    //Quality dropdown menu
    public Dropdown qualityDD;
    //Difficulty dropdown menu
    public Dropdown difficultyDD;
    
    void Start()
    {
        qualityDD.value = QualitySettings.GetQualityLevel();
        difficultyDD.value = BetterGameManager.difficulty;
    }

}
