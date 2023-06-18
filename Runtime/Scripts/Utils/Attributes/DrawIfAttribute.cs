using UnityEngine;
using System;

public enum E_ComparisonType
{
    Property,
    Value
}

/// <summary>
/// Draws the field/property ONLY if the value of the comparedPropertyName is matching with the comparedValue.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class DrawIfAttribute : PropertyAttribute
{
    #region Fields
    
    public string firstProperty { get; private set; }
    public string secondProperty { get; private set; }
    public object compareValue{ get; private set; }
    public E_DisablingType disablingType { get; private set; }
    public E_ComparisonType comparisonType { get; private set; }

    #endregion

    /// <summary>
    /// Constructor. This method is the one that will gonna describe our attribute and decide which parameters we need to use.
    /// Only draws the field if a condition is met. Supports enum and bools (but we can add other types of conditions such as comparison).
    /// </summary>
    /// <param name="firstPropertyName">The name of the property that is being compared (case sensitive).</param>
    /// <param name="compared_PropertyB">The name of the second property to being compared to.</param>
    /// <param name="disablingType">The type of disabling that should happen if the condition is NOT met. Defaulted to DisablingType.DontDraw.</param>
    public DrawIfAttribute(string firstProperty, string secondProperty, E_DisablingType disablingType)
    {
        this.firstProperty = firstProperty;
        this.secondProperty = secondProperty;
        this.disablingType = disablingType;
        this.comparisonType = E_ComparisonType.Property;
    }
    
    public DrawIfAttribute(string property, object compareValue, E_DisablingType disablingType)
    {
        this.firstProperty = property;
        this.compareValue = compareValue;
        this.disablingType = disablingType;
        this.comparisonType = E_ComparisonType.Value;
    }
}