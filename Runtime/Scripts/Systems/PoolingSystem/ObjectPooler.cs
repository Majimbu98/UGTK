// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Manages a pool of reusable objects.
    /// </summary>
    public abstract class ObjectPooler<T> : MonoBehaviour where T: MonoBehaviour,IPoolable
    {
        #region Variables & Properties

        [Header("Object")]
        [SerializeField]
        public GameObject objectPoolable;

        [Header("The parent objects to attach the poolable object")]
        public GameObject activatedParent;
        public GameObject deactivatedParent;

        [Header("Extra Options")]
        
        [SerializeField]
        public bool useHisTransform = true;
        
        [DrawIf("useHisTransform", false, E_DisablingType.DontDraw)]
        public Transform transformObject;
        
        [SerializeField]
        public bool useCustomDieTime = false;

        [DrawIf("useCustomDieTime", true, E_DisablingType.DontDraw)]
        public float dieTime;

        // Action to be executed by the object after the dieTime has passed.
        private Action actionToDoAfterDieTime;

        [Header("Quantity of the objects")]
        public int quantity;

        [Header("Whether the quantity list is expandable or not")]
        public bool expandable;

        private List<IPoolable> objectPoolables = new List<IPoolable>();

        #endregion

        #region MonoBehaviour

        // Summary:
        // Awake is called when the script instance is being loaded.
        protected void Start()
        {
            InitializePool();
        }

        #endregion

        #region Methods

        #region Editor Methods
        
        private void OnValidate()
        {
            OnObjectPoolableChanged();
            Check();
        }

        private void Check()
        {
            if (useCustomDieTime == false)
            {
                dieTime = 0;
            }

            if (useHisTransform == true && objectPoolable != null)
            {
                transformObject = objectPoolable.transform;
            }

            if (useHisTransform == false)
            {
                transformObject = null;
            }
        }

        /// <summary>
        /// Callback method called when the objectPoolable property is changed.
        /// </summary>
        public void OnObjectPoolableChanged()
        {
            if (objectPoolable != null)
            {
                // Check if objectPoolable has the T component attached
                if (objectPoolable.GetComponent<T>() == null)
                {
                    Debug.Log(("Error! Use a GameObject with " + typeof(T).ToString() + " component attached."));
                    objectPoolable = null;
                }
            }
        }
        
        #endregion
        
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
            GameObject gameObject = Instantiate(objectPoolable);
            
            IPoolable poolable = GetIPoolableFrom(gameObject);
            
            poolable.Initialize(gameObject, this);
            
            return poolable;
        }

        public void SetNewDieTimeForAll(float _dieTime)
        {
            dieTime = _dieTime;
            if (dieTime == 0)
            {
                useCustomDieTime = false;
            }

            foreach (IPoolable poolable in objectPoolables)
            {
                poolable.SetDieTime(dieTime);
            }
        }
    

        public void SetNewActiveParentForAll(GameObject _activatedParent)
        {
            activatedParent = _activatedParent;
            foreach (IPoolable poolable in objectPoolables)
            {
                poolable.SetActivatedParent(activatedParent);
            }
        }

        public void SetNewDeactivatedParentForAll(GameObject _deactivatedParent)
        {
            deactivatedParent = _deactivatedParent;
            foreach (IPoolable poolable in objectPoolables)
            {
                poolable.SetActivatedParent(deactivatedParent);
            }
        }

        public void SetNewDieTimeForNext(float _dieTime)
        {
            IPoolable poolable = GetFirstPooledObject();
            poolable.SetDieTime(_dieTime);
        }
        
        public void SetNewActiveParentForNext(GameObject _activatedParent)
        {
            IPoolable poolable = GetFirstPooledObject();
            poolable.SetActivatedParent(_activatedParent);
        }

        public void SetNewDeactivatedParentForNext(GameObject _deactivatedParent)
        {
            IPoolable poolable = GetFirstPooledObject();
            poolable.SetActivatedParent(_deactivatedParent);
        }
        
        /// <summary>
        /// Retrieves the IPoolable component from the given GameObject.
        /// </summary>
        private IPoolable GetIPoolableFrom(GameObject gameObject)
        {
            return gameObject.GetComponentInChildren<IPoolable>();
        }

        /// <summary>
        /// Retrieves the first inactive pooled object from the pool.
        /// </summary>
        /// <returns>The first inactive pooled object or null if the pool is not expandable.</returns>
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
        /// <param name="poolable">The IPoolable object to spawn.</param>
        public GameObject SpawnPoolable(IPoolable poolable)
        {
            poolable.Spawn();
            return poolable.self;
        }
        
        /// <summary>
        /// Spawns a poolable object from the pool and sets its temporary dieTime.
        /// </summary>
        /// <param name="poolable">The IPoolable object to spawn.</param>
        /// <param name="dieTime">The temporary die time for the spawned object.</param>
        /// <returns>The spawned GameObject.</returns>
        public GameObject SpawnPoolable(IPoolable poolable ,float dieTime)
        {
            SetNewDieTimeForNext(dieTime);
            poolable.Spawn();
            return poolable.self;
        }

        #endregion
    }
}