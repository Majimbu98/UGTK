// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Manages a pool of reusable objects.
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        #region Variables & Properties

        [Header("Object")]
        [SerializeField] private ObjectToPool objectPoolableInfo;

        [Header("Quantity of the objects")]
        public int quantity;

        [Header("Whether the quantity list is expandable or not")]
        public bool expandable;

        private List<IPoolable> objectPoolables = new List<IPoolable>();

        #endregion

        #region MonoBehaviour

        // Awake is called when the script instance is being loaded
        protected void Awake()
        {
            InitializePool();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the object pool by creating the initial quantity of objects.
        /// </summary>
        private void InitializePool()
        {
            for (int i = 0; i < quantity; i++)
            {
                InitializeSinglePooledObject();
            }
        }

        /// <summary>
        /// Initializes a single object in the pool.
        /// </summary>
        private GameObject InitializeSinglePooledObject()
        {
            GameObject gameObject = Instantiate(objectPoolableInfo.objectPoolable);
            IPoolable poolable = gameObject.GetComponentInChildren<IPoolable>();
            poolable.Initialize(gameObject, objectPoolableInfo);
            objectPoolables.Add(poolable);
            return gameObject;
        }

        /// <summary>
        /// Sets the die time for a poolable object.
        /// </summary>
        protected virtual void SetDieTime(IPoolable poolable)
        {

        }

        /// <summary>
        /// Retrieves the first active pooled object from the pool.
        /// </summary>
        protected GameObject GetFirstPooledObject()
        {
            foreach (IPoolable poolable in objectPoolables)
            {
                if (poolable.IsActive())
                {
                    return poolable.self;
                }
            }

            if (expandable)
            {
                return InitializeSinglePooledObject();
            }
            else
            {
                Debug.LogError("Error! Cannot add " + objectPoolables[0].GetType().ToString() + " type in " + gameObject.name + " pooler list.");
                return null;
            }
        }

        /// <summary>
        /// Spawns a poolable object from the pool.
        /// </summary>
        protected GameObject SpawnPoolable()
        {
            return GetFirstPooledObject();
        }

        #endregion
    }
}