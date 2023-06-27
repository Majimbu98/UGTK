// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents data with a key-value pair and provides methods for manipulating and comparing the data.
    /// </summary>
    [System.Serializable]
    public class Data<T> : Content<Data<T>>
    {
        [SerializeField] private string nameVariable;
        [SerializeField] public T currentValue; // The key-value pair representing the data.
        [SerializeField] public T defaultValue; // The default value for the data.
        [SerializeField] private bool resetDefaultValueOnPlayMode = false;

        public override void Init()
        {
            if (Application.isPlaying)
            {
                if (resetDefaultValueOnPlayMode)
                {
                    SetToDefaultValue();
                }
            }
        }

        /// <summary>
        /// Sets the data value to the default value.
        /// </summary>
        public void SetToDefaultValue()
        {
            currentValue = defaultValue;
        }
    }
}
