// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents a poolable visual effects (VFX) object.
    /// </summary>
    public class VFXPoolable : MonoBehaviour, IPoolable
    {
        #region Variables & Properties
        
        [HideInInspector]
        public ParticleSystem particleSystem;

        #endregion

        // Region containing MonoBehaviour lifecycle events
        #region MonoBehaviour
    
        /// <summary>
        /// Called when the script is loaded into the Unity editor.
        /// </summary>
        void Awake()
        {
            // Getting the ParticleSystem component attached to this object
            particleSystem = GetComponent<ParticleSystem>();
        }

        #endregion

        
        #region Interface
        
        public GameObject self { get; set; }
        public Transform transformObject { get; set; }
        public GameObject parentWhenActivated { get; set; }
        public GameObject parentWhenDeactivated { get; set; }
        public float dieTime { get; set; }
        public Action actionOnDespawn { get; set; }
    
        #endregion
    }
}