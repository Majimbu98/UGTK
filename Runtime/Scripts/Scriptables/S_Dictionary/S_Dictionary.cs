// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public abstract class S_Dictionary<T, T1> : ScriptableObject
    {
        // Defines variables and properties
        #region Variables & Properties
        
        [SerializeField] public List<BiClass<T, T1>> list;

        #endregion

    }
}
