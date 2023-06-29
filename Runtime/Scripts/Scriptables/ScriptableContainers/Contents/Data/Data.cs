// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents data with a key-value pair and provides methods for manipulating and comparing the data.
    /// </summary>
    [System.Serializable]
    public class Data<T> : Content<Data<T>>, IExecutableOnPlay
    {
        [SerializeField] private string nameVariable;
        [SerializeField] public T currentValue; // The key-value pair representing the data.
        [SerializeField] public T defaultValue; // The default value for the data.
        [SerializeField] public bool resetValueOnPlay = false;
        
        public override void Init()
        {
            if (resetValueOnPlay) 
            { 
                SetToDefaultValue();
            }
        }

        public Data()
        {
            //add
        }

        /// <summary>
        /// Sets the data value to the default value.
        /// </summary>
        public void SetToDefaultValue()
        {
            currentValue = defaultValue;
        }
        
        public void OnPlay()
        {
            Init();
        }
    }
}
