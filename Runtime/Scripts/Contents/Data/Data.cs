// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents data with a key-value pair and provides methods for manipulating and comparing the data.
    /// </summary>
    [System.Serializable]
    public class Data : Content<Data>
    {
        [SerializeField] BiClass<string, float> data; // The key-value pair representing the data.
        [SerializeField] private float defaultValue; // The default value for the data.

        /// <summary>
        /// Sets the data value to the default value.
        /// </summary>
        public void SetToDefaultValue()
        {
            data.secondValue = defaultValue;
        }

        /// <summary>
        /// Increases the data value by the specified amount.
        /// </summary>
        /// <param name="increaseValue">The amount to increase the data value by.</param>
        public void IncreaseDataOf(float increaseValue)
        {
            data.secondValue += increaseValue;
        }

        /// <summary>
        /// Decreases the data value by the specified amount.
        /// </summary>
        /// <param name="decreaseValue">The amount to decrease the data value by.</param>
        public void DecreaseDataOf(float decreaseValue)
        {
            data.secondValue -= decreaseValue;
        }

        /// <summary>
        /// Checks if the specified value is a new record compared to the current data value.
        /// </summary>
        /// <param name="value">The value to compare with the data value.</param>
        /// <returns>True if the specified value is greater than the data value, false otherwise.</returns>
        public bool IsRecord(float value)
        {
            return value > data.secondValue;
        }

        /// <summary>
        /// Sets a new record if the specified value is greater than the current data value.
        /// </summary>
        /// <param name="record">The new record value.</param>
        public void SetNewRecord(float record)
        {
            if (IsRecord(record))
            {
                data.secondValue = record;
            }
            else
            {
                Debug.Log("Error! It's not a new record!");
            }
        }
    }
}
