// © 2023 Marcello De Bonis. All rights reserved.

using System;
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
        protected void Start()
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
                objectPoolables.Add(InitializeSinglePooledObject());
            }
        }

        /// <summary>
        /// Initializes a single object in the pool.
        /// </summary>
        private IPoolable InitializeSinglePooledObject()
        {
            GameObject gameObject = Instantiate(objectPoolableInfo.objectPoolable);
            IPoolable poolable = GetIPoolableFrom(gameObject);
            poolable.Initialize(gameObject, objectPoolableInfo);
            return poolable;
        }

        private IPoolable GetIPoolableFrom(GameObject gameObject)
        {
            return gameObject.GetComponentInChildren<IPoolable>();
        }

        /// <summary>
        /// Retrieves the first active pooled object from the pool.
        /// </summary>
        public IPoolable GetFirstPooledObject()
        {
            foreach (IPoolable poolable in objectPoolables)
            {
                if (!poolable.IsActive())
                {
                    return poolable;
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
        public GameObject SpawnPoolable(IPoolable poolable)
        {
            poolable.Spawn();
            return poolable.self;
        }
        
        /// <summary>
        /// Spawns a poolable object from the pool and set his temporary dieTime.
        /// </summary>
        public GameObject SpawnPoolable(IPoolable poolable ,float dieTime)
        {
            poolable.SetDieTime(dieTime);
            poolable.Spawn();
            return poolable.self;
        }

        #endregion
    }
}