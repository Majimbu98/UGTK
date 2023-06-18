// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using System;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Custom attribute used to conditionally draw a property in the Unity inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DrawIfAttribute : PropertyAttribute
    {
        #region Fields

        public string firstProperty { get; private set; }
        public string secondProperty { get; private set; }
        public object compareValue { get; private set; }
        public E_DisablingType disablingType { get; private set; }
        public E_ComparisonType comparisonType { get; private set; }

        #endregion
        /// <summary>
        /// Constructs a DrawIfAttribute to compare two properties.
        /// </summary>
        public DrawIfAttribute(string firstProperty, string secondProperty, E_DisablingType disablingType)
        {
            this.firstProperty = firstProperty;
            this.secondProperty = secondProperty;
            this.disablingType = disablingType;
            this.comparisonType = E_ComparisonType.Property;
        }

        /// <summary>
        /// Constructs a DrawIfAttribute to compare a property with a value.
        /// </summary>
        public DrawIfAttribute(string property, object compareValue, E_DisablingType disablingType)
        {
            this.firstProperty = property;
            this.compareValue = compareValue;
            this.disablingType = disablingType;
            this.comparisonType = E_ComparisonType.Value;
        }
    }
}