// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        public GameObject activatedParent;
        public GameObject deactivatedParent;

        [Header("Transform for the objects")]
        public Transform transformObject;

        [Header("The time after the object deactivates. Set to 0 if not used.")]
        public float dieTime;
        
        //If dieTime is different from 0, it will be an action that the object will do after deactivated
        private Action actionToDoAfterDieTime;
    }
}