// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

/// <summary>
/// Interface for objects that can be compared.
/// </summary>
public interface IComparableObject
{
    /// <summary>
    /// Gets or sets the value used for comparison.
    /// </summary>
    int CompareValue { get; set; }
}