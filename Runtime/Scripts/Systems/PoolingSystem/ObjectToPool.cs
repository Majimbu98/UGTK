// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{

    [System.Serializable]
    public class ObjectToPool
    {
        public GameObject objectPoolable;
        public GameObject parentWhenDeactivated;
        public GameObject parentWhenActivated;
        public int quantity;
        public bool expandable;
        public Transform transform;
    }

}