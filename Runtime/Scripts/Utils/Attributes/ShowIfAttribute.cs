// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using System;

namespace UnityGamesToolkit.Runtime
{
    using UnityEngine;
    using System;
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ShowIfAttribute : PropertyAttribute
    {
        #region Fields
        public string comparedPropertyName { get; private set; }
        public string comparedValue { get; private set; }
        public E_DisablingType disablingType { get; private set; }
        public E_ComparisonType comparisonType { get; private set; }
        #endregion

      
        public ShowIfAttribute(string comparedPropertyName, string comparedValue, E_DisablingType disablingType, E_ComparisonType comparisonType = E_ComparisonType.EQUALS)
        {
            this.comparedPropertyName = comparedPropertyName;
            this.comparedValue = comparedValue;
            this.disablingType = disablingType;
            this.comparisonType = comparisonType;
        }
    }
}