// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Interface for poolable objects.
    /// </summary>
    public interface IPoolable
    {
        // Defines variables and properties
        #region Variables & Properties
        
        public GameObject self { get; set; }
        public Transform transformObject{ get; set; }
        public GameObject parentWhenActivated { get; set; }
        public GameObject parentWhenDeactivated { get; set; }
        public float dieTime { get; set; }
        public Action actionOnSpawn { get; set; }
        public Action actionOnDespawn { get; set; }
        
        #endregion

        // Defines methods for the script
        #region Methods

        /// <summary>
        /// Attaches the object to the parent when activated.
        /// </summary>
        public void AttachToActivatedParent()
        {
            self.transform.parent = parentWhenActivated.transform;
        }

        /// <summary>
        /// Attaches the object to the parent when deactivated.
        /// </summary>
        public void AttachToDeactivatedParent()
        {
            self.transform.parent = parentWhenDeactivated.transform;
        }

        /// <summary>
        /// Checks if the object is currently active.
        /// </summary>
        public bool IsActive()
        {
            return self.activeInHierarchy;
        }

        /// <summary>
        /// Initializes the poolable object.
        /// </summary>
        public void Initialize(GameObject obj, ObjectToPool objectToPool)
        {
            self = obj;
            parentWhenActivated = objectToPool.activatedParent;
            parentWhenDeactivated = objectToPool.deactivatedParent;
            dieTime = objectToPool.dieTime;
            transformObject = objectToPool.transformObject;
            Despawn();
        }
        
        /// <summary>
        /// Spawns the poolable object.
        /// </summary>
        public void Spawn()
        {
            self.SetActive(true);
            SetTransform();
            AttachToActivatedParent();
            actionOnSpawn?.Invoke();
            if (dieTime != 0)
            {
                Timer.DoAfterTime(dieTime, Despawn);
            }
        }

        /// <summary>
        /// Despawns the poolable object.
        /// </summary>
        public void Despawn()
        {
            if (IsActive())
            {
                actionOnDespawn?.Invoke();
                AttachToDeactivatedParent();
                self.SetActive(false);
            }
        }

        /// <summary>
        /// Sets the die time for the poolable object.
        /// </summary>
        public void SetDieTime(float _dieTime)
        {
            dieTime = _dieTime;
        }

        public void SetActionOnSpawn(Action _onSpawn)
        {
            actionOnSpawn = _onSpawn;
        }
        
        public void SetActionOnDespawn(Action _onDespawn)
        {
            actionOnDespawn = _onDespawn;
        }

        /// <summary>
        /// Sets the position and rotation of the poolable object.
        /// </summary>
        private void SetTransform()
        {
            self.transform.position= transformObject.position;
            self.transform.rotation = transformObject.rotation;
        }

        #endregion

    }
}
