using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryController : MonoBehaviour
{
    //Text object to display score
    public Text txt;
    
    void Start()
    {
        txt.text = "" + (int)BetterGameManager.gameScore;
    }

}
