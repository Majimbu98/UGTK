// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using System;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Attribute to show or hide a property based on a specified condition.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ShowIfAttribute : PropertyAttribute
    {
        #region Variables & Properties

        /// <summary>
        /// The name of the method that returns a boolean to determine the visibility of the property.
        /// </summary>
        public string boolMethod { get; private set; }

        /// <summary>
        /// The type of disabling to apply when the condition is not met.
        /// </summary>
        public E_DisablingType disablingType { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ShowIfAttribute class with the specified method name and disabling type.
        /// </summary>
        /// <param name="boolMethod">The name of the method that returns a boolean to determine the visibility of the property.</param>
        /// <param name="disablingType">The type of disabling to apply when the condition is not met.</param>
        public ShowIfAttribute(string boolMethod, E_DisablingType disablingType = E_DisablingType.READONLY)
        {
            this.boolMethod = boolMethod;
            this.disablingType = disablingType;
        }

        #endregion
    }
}