using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; }

    [SerializeField] private int baseCost;
    [SerializeField] private float costGrowthPerLevel;
    [SerializeField] private GrowthFormula costGrowthFormula;

    [SerializeField] private float baseEffect, effectGrowthPerLevel;
    [SerializeField] private GrowthFormula effectGrowthFormula;

    [field: SerializeField, Tooltip("Should this upgrade affect any stats? (Set to false for one-time unlock upgrades)")] 
    public bool AffectsStats { get; private set; } = true;
    public StatModifier.Type statType;
    [field: SerializeField] public string AffectedStat { get; private set; }
    
    private enum GrowthFormula
    {
        Additive, //X + gpl * level
        Multiplicative, //X + gpl ^ level
        Percentage, //X * gpl ^ level
        Exponential //X ^ level
    }
    
    public float GetEffect(int level) => 
        CalculateWithFormula(baseEffect, effectGrowthPerLevel, level, effectGrowthFormula);
    
    public int GetCost(int currentLevel) => 
        currentLevel == 0
            ? baseCost 
            : (int)Mathf.Floor(CalculateWithFormula(baseCost, 
                costGrowthPerLevel, 
                currentLevel, 
                costGrowthFormula));
    
    /// <summary>
    /// Calculates the final value using one of the formulas.
    /// </summary>
    /// <param name="baseValue">The value the effect starts with.</param>
    /// <param name="growthPerLevel">How much does value grow per level.</param>
    /// <param name="level">The level to calculate the result for.</param>
    /// <param name="formulaType"></param>
    /// <returns>The calculated result.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static float CalculateWithFormula(float baseValue, float growthPerLevel, float level, GrowthFormula formulaType)
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
