using System;

public class StatModifier
{
    public enum Type
    {
        Flat,
        Additive,
        Multiplicative
    }
    
    public float Value { get; private set; }
    public readonly Type ModifierType;
    public Action<float, float> Updated;
    
    public StatModifier(float value, Type modifierType)
    {
        Value = value;
        ModifierType = modifierType;
    }

    public void Update(float newValue)
    {
        float oldVal = Value;
        Value = newValue;
        Updated?.Invoke(oldVal, Value);
    }
}