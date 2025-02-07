using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stat
{
    //This Stat system makes it easy to calculate the modifiers, although it's not flexible when it comes to other types
    //of modifiers, i.e. if we want to have a special modifier after flat but before additive modifiers. It will involve
    //an addition of the new calculation and variables everywhere. I've built a similar system before and it takes quite
    //a while to make it work.
    
    
    //The base value of the modifier. Assigned in the constructor.
    private readonly float _baseValue;

    private readonly List<StatModifier> _flatModifiers = new();
    private readonly List<StatModifier> _additiveModifiers = new();
    private readonly List<StatModifier> _multiplicativeModifiers = new();
    
    //Cached values, since these values will be accessed frequently. We don't want to reiterate all 3 lists every frame
    private float _flatModifiersValue;
    private float _additiveModifiersValue;
    private float _multiplicativeModifiersValue;

    public Stat(float baseValue)
    {
        _baseValue = baseValue;
    }
    
    /// <summary>
    /// Value of the stat after applying all modifiers.
    /// The stat calculation formula is as follows:
    /// (Base + Flat + Additive * Base) * Multiplicative
    /// </summary>
    public float Value => (_baseValue + _flatModifiersValue + _additiveModifiersValue * _baseValue) 
                          * (1 + _multiplicativeModifiersValue);

    //Adds a modifier reference
    public void AddModifier(in StatModifier modifier)
    {
        switch (modifier.ModifierType)
        {
            case StatModifier.Type.Flat:
                modifier.Updated += UpdateFlat;
                _flatModifiers.Add(modifier);
                _flatModifiersValue = _flatModifiers.Sum(x => x.Value);
                break;
            case StatModifier.Type.Additive:
                modifier.Updated += UpdateAdditive;
                _additiveModifiers.Add(modifier);
                _additiveModifiersValue = _additiveModifiers.Sum(x => x.Value);
                break;
            case StatModifier.Type.Multiplicative:
                modifier.Updated += UpdateMultiplicative;
                _multiplicativeModifiers.Add(modifier);
                _multiplicativeModifiersValue = _multiplicativeModifiers.Aggregate(1f, (acc, x) => acc * x.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void RemoveModifier(in StatModifier modifier)
    {
        switch (modifier.ModifierType)
        {
            case StatModifier.Type.Flat:
                modifier.Updated -= UpdateFlat;
                _flatModifiers.Remove(modifier);
                _flatModifiersValue = _flatModifiers.Sum(x => x.Value);
                break;
            case StatModifier.Type.Additive:
                modifier.Updated -= UpdateAdditive;
                _additiveModifiers.Remove(modifier);
                _additiveModifiersValue = _additiveModifiers.Sum(x => x.Value);
                break;
            case StatModifier.Type.Multiplicative:
                modifier.Updated -= UpdateMultiplicative;
                _multiplicativeModifiers.Remove(modifier);
                _multiplicativeModifiersValue = _multiplicativeModifiers.Aggregate(1f, (acc, x) => acc * x.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //Simply updates the value. Quick way for simple updates
    private void UpdateFlat(float oldValue, float newValue)
    {
        _flatModifiersValue += newValue - oldValue;
        Debug.Log("Flat update: " + _flatModifiersValue);
    }
    
    private void UpdateAdditive(float oldValue, float newValue)
    {
        _additiveModifiersValue += newValue - oldValue;
    }
    
    private void UpdateMultiplicative(float oldValue, float newValue)
    {
        _multiplicativeModifiersValue = _multiplicativeModifiers.Aggregate(1f, (acc, x) => acc * x.Value);
    }
}
