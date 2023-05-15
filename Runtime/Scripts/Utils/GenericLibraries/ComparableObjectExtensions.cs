// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

/// <summary>
/// Extension methods for the IComparableObject interface.
/// </summary>
public static class ComparableObjectExtensions
{
    #region Comparison Operators

    /// <summary>
    /// Determines whether one object is less than another object.
    /// </summary>
    /// <param name="obj1">The first object to compare.</param>
    /// <param name="obj2">The second object to compare.</param>
    /// <returns>True if obj1 is less than obj2; otherwise, false.</returns>
    public static bool LessThan(this IComparableObject obj1, IComparableObject obj2)
    {
        return obj1.CompareValue < obj2.CompareValue;
    }

    /// <summary>
    /// Determines whether one object is greater than another object.
    /// </summary>
    /// <param name="obj1">The first object to compare.</param>
    /// <param name="obj2">The second object to compare.</param>
    /// <returns>True if obj1 is greater than obj2; otherwise, false.</returns>
    public static bool GreaterThan(this IComparableObject obj1, IComparableObject obj2)
    {
        return obj1.CompareValue > obj2.CompareValue;
    }

    /// <summary>
    /// Determines whether one object is less than or equal to another object.
    /// </summary>
    /// <param name="obj1">The first object to compare.</param>
    /// <param name="obj2">The second object to compare.</param>
    /// <returns>True if obj1 is less than or equal to obj2; otherwise, false.</returns>
    public static bool LessThanOrEqual(this IComparableObject obj1, IComparableObject obj2)
    {
        return obj1.CompareValue <= obj2.CompareValue;
    }

    /// <summary>
    /// Determines whether one object is greater than or equal to another object.
    /// </summary>
    /// <param name="obj1">The first object to compare.</param>
    /// <param name="obj2">The second object to compare.</param>
    /// <returns>True if obj1 is greater than or equal to obj2; otherwise, false.</returns>
    public static bool GreaterThanOrEqual(this IComparableObject obj1, IComparableObject obj2)
    {
        return obj1.CompareValue >= obj2.CompareValue;
    }

    /// <summary>
    /// Determines whether two objects are equal.
    /// </summary>
    /// <param name="obj1">The first object to compare.</param>
    /// <param name="obj2">The second object to compare.</param>
    /// <returns>True if obj1 is equal to obj2; otherwise, false.</returns>
    public static bool Equals(this IComparableObject obj1, IComparableObject obj2)
    {
        return obj1.CompareValue == obj2.CompareValue;
    }

    #endregion
}