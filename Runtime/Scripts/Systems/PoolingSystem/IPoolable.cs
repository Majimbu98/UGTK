// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections.Generic;
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
        public void Initialize<T>(GameObject obj, ObjectPooler<T> pooler) where T: MonoBehaviour, IPoolable
        {
            self = obj;

            SetActivatedParent(pooler.activatedParent);
            SetDeactivatedParent(pooler.deactivatedParent);

            if (pooler.useCustomDieTime)
            {
                SetDieTime(pooler.dieTime);
            }

            if (!pooler.useHisTransform)
            {
                SetTransform(pooler.transformObject);
            }
            
            Despawn();
        }
        
        /// <summary>
        /// Spawns the poolable object.
        /// </summary>
        public void Spawn()
        {
            self.SetActive(true);
            ChangeTransform();
            AttachToActivatedParent();
            if (dieTime != 0)
            {
                TimerSpawner.DoAfterTime(dieTime, Despawn);
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
        /// Sets the position and rotation of the poolable object.
        /// </summary>
        private void ChangeTransform()
        {
            self.transform.position= transformObject.position;
            self.transform.rotation = transformObject.rotation;
        }

        public void SetActivatedParent(GameObject _parentWhenActivated)
        {
            parentWhenActivated = _parentWhenActivated;
        }
        
        public void SetDeactivatedParent(GameObject _deparentWhenActivated)
        {
            parentWhenDeactivated = _deparentWhenActivated;
        }
        
        /// <summary>
        /// Sets the position and rotation of the poolable object.
        /// </summary>
        public void SetTransform(Transform _transformObject)
        {
            transformObject = _transformObject;
        }

        /// <summary>
        /// Sets the die time for the poolable object.
        /// </summary>
        public void SetDieTime(float _dieTime)
        {
            dieTime = _dieTime;
        }

        public void SetActionOnDespawn(Action _onDespawn)
        {
            actionOnDespawn = _onDespawn;
        }
        
        #endregion

    }
}
