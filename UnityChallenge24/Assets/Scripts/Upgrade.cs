using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeData data;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private StatsRef playerStats;

    private StatModifier _currentModifier;
    private int _currentLevel;

    private void Start()
    {
        _currentModifier = new StatModifier(data.GetEffect(1), data.statType);
        Display();
    }

    private void Display()
    {
        costText.text = data.GetCost(_currentLevel).ToString("N");
        levelText.text = $"{_currentLevel}/{data.MaxLevel}";
        descriptionText.text = 
            data.Description.Replace("X", $"<color=orange>{data.GetEffect(_currentLevel)}</color>");
    }

    public void Buy()
    {
        if (_currentLevel < data.MaxLevel && Player.Instance.Coins >= data.GetCost(_currentLevel))
        {
            Player.Instance.Pay(data.GetCost(_currentLevel));
            _currentLevel++;
            
            //For the first level add it
            if (_currentLevel == 1)
            {
                playerStats.GetStat(data.AffectedStat, out Stat stat);
                stat.AddModifier(_currentModifier);
            }
            else
            {
                _currentModifier.Update(data.GetEffect(_currentLevel));
            }
            
            Display();
        }
    }
}
