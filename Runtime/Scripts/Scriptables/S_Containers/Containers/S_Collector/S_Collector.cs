// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Class representing a collector of ScriptableObject objects.
    /// </summary>
    /// <typeparam name="T">The type of ScriptableObject that can be collected.</typeparam>
    public abstract class S_Collector<T> : ScriptableObject
    {
        // Variables and properties

        #region Variables & Properties

        [SerializeField] public List<T> list;

        #endregion
    }
}