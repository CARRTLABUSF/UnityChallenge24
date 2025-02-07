using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [field: SerializeField]
    public string Description { get; private set; }
    
    [field: SerializeField]
    public int MaxLevel { get; private set; }

    [SerializeField] private int baseCost;
    [SerializeField] private float costGrowthPerLevel;
    [SerializeField] private GrowthFormula costGrowthFormula;
    public int GetCost(int currentLevel) => 
        currentLevel == 0
            ? baseCost 
            : (int)Mathf.Floor(CalculateWithFormula(baseCost, 
                                                      costGrowthPerLevel, 
                                                      currentLevel, 
                                                      costGrowthFormula));

    [SerializeField] private float baseEffect, effectGrowthPerLevel;
    [SerializeField] private GrowthFormula effectGrowthFormula;
    public StatModifier.Type statType;
    
    [field: SerializeField]
    public string AffectedStat { get; private set; }
    
    [SerializeField] private StatsRef statsRef;
    
    private enum GrowthFormula
    {
        Additive, //X + gpl * level
        Multiplicative, //X + gpl ^ level
        Percentage, //X * gpl ^ level
        Exponential //X ^ level
    }
    
    public float GetEffect(int level) => 
        level == 0
            ? 0 
            : CalculateWithFormula(baseEffect, effectGrowthPerLevel, level, effectGrowthFormula);
    
    
    private float CalculateWithFormula(float baseValue, float growthPerLevel, float level, GrowthFormula formulaType)
    {
        return formulaType switch
        {
            GrowthFormula.Exponential    => Mathf.Pow(baseValue, level),
            GrowthFormula.Multiplicative => baseValue + Mathf.Pow(growthPerLevel, level),
            GrowthFormula.Percentage     => baseValue * Mathf.Pow(growthPerLevel, level),
            GrowthFormula.Additive       => baseValue + growthPerLevel * level,
            _ => throw new ArgumentOutOfRangeException(nameof(formulaType), formulaType, null)
        };
    }

}
