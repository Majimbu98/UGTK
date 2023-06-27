// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    [Serializable]
    public class Container<T>
    {
        public List<T> list;
    }
}
