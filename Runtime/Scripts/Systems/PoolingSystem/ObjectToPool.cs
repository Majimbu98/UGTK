// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Configuration class for objects to be pooled.
    /// </summary>
    [System.Serializable]
    public class ObjectToPool
    {
        [Header("The Object to be pooled")]
        public GameObject objectPoolable;

        [Header("The parent objects to attach the poolable object")]
        public GameObject parentWhenDeactivated;
        public GameObject parentWhenActivated;

        [Header("Transform for the objects")]
        public Transform transformObject;

        [Header("The time after the object deactivates. Set to 0 if not used.")]
        public float dieTime;
    }
}