// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public class S_BiClass_Collector<T, T1> : ScriptableObject
    {

        // Defines variables and properties
        #region Variables & Properties
        
        [SerializeField] public List<BiClass<T, T1>> list;

        #endregion

    }
}
