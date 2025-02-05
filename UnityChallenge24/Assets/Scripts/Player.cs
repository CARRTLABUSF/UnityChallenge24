using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public long Coins => _coins;
    
    private long _coins;

    [SerializeField] private TextMeshProUGUI coinsLabel;


    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin()
    {
        _coins++;
        coinsLabel.text = _coins.ToString();
    }
}
