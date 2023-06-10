using UnityEngine;
using System;

/// <summary>
/// Draws the field/property ONLY if the value of the comparedPropertyName is matching with the comparedValue.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ShowIfAttribute : PropertyAttribute
{
    #region Fields

    public string comparedPropertyName { get; private set; }
    public string comparedValue { get; private set; }
    public DisablingType disablingType { get; private set; }
    public ComparisonType comparisonType { get; private set; }
    public string similarValue { get; private set; }
    #endregion

    /// <summary>
    /// Constructor. This method is the one that will gonna describe our attribute and decide which parameters we need to use.
    /// Only draws the field if a condition is met. Supports enum and bools (but we can add other types of conditions such as comparison).
    /// </summary>
    /// <param name="comparedPropertyName">The name of the property that is being compared (case sensitive).</param>
    /// <param name="comparedValue">The value the property is being compared to.</param>
    /// <param name="disablingType">The type of disabling that should happen if the condition is NOT met. Defaulted to DisablingType.DONTSHOW.</param>
    /// <param name="comparisonType">The type of comparison between values. Defaulted to ComparisonType.EQUALS.</param>
    /// <param name="similarValue">If ComparisonType.SIMILAR_VALUE, check the value +/- this value</param>
    public ShowIfAttribute(string comparedPropertyName, string comparedValue, DisablingType disablingType, ComparisonType comparisonType = ComparisonType.EQUALS, string similarValue = "")
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
        this.comparisonType = comparisonType;
        this.similarValue = similarValue;
    }
}

/// <summary>
/// What to do with the variable.
/// </summary>
public enum DisablingType
{
    READONLY,//The field is show disabled
    DONTSHOW//The field is not show
}

/// <summary>
/// Types of comparisons.
/// </summary>
public enum ComparisonType
{
    EQUALS,
    NOT_EQUALS,
    GREATER,
    LESSER,
    EQUALS_GREATER,
    EQUALS_LESSER,
    SIMILAR_VALUE,
}