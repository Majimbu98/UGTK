// © 2023 Marcello De Bonis. All rights reserved.

using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace UnityGamesToolkit.Runtime
{
    public class ColliderDetector : MonoBehaviour
    {
        // Defines variables and properties
        #region Variables & Properties

        [SerializeField] public E_VerifyCollision typeCollision;

        public GameObject gameObjectReferenceLastFrame;
        public GameObject gameObjectReference;
        public Container<Component> componentList;
        public string tag;

        private Collider collider;

        public UnityEvent<Collision> onCollisionEnter;
        public UnityEvent<Collision> onCollisionStay;
        public UnityEvent<Collision> onCollisionExit;

        #endregion

        // Defines methods for the new script

        #region Methods

        private void Awake()
        {
            collider = GetComponent<Collider>();
        }

        private void OnValidate()
        {
            UpgradeGameObjectReference();
            UpgradeComponentList();
            UpgradeTag();
        }

        private void UpgradeGameObjectReference()
        {
            if (typeCollision != E_VerifyCollision.ObjectComponents && typeCollision !=E_VerifyCollision.DirectObject)
            {
                gameObjectReference = null;
            }
        }
        
        private void UpgradeComponentList()
        {
            if (gameObjectReferenceLastFrame != gameObjectReference)
            {
                componentList.list.Clear();
                if (gameObjectReference != null)
                {
                    componentList.list = gameObjectReference.GetComponents<Component>().ToList();
                }
            }

            gameObjectReferenceLastFrame = gameObjectReference;
        }
        
        private void UpgradeTag()
        {
            if (typeCollision != E_VerifyCollision.Tag)
            {
                tag = "";
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (ConditionCollision(other))
            {
                onCollisionEnter?.Invoke(other);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (ConditionCollision(other))
            {
                onCollisionStay?.Invoke(other);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (ConditionCollision(other))
            {
                onCollisionExit?.Invoke(other);
            }
        }

        private bool ConditionCollision(Collision other)
        {
            GameObject otherObject = other.gameObject;
            
            switch (typeCollision)
            {
                case E_VerifyCollision.ObjectComponents:

                    return (ComponentUtility.CompareComponentLists(gameObjectReference, otherObject));
                        
                    break;
                case E_VerifyCollision.Tag:

                    return gameObject.tag == otherObject.tag;

                    break;
                case E_VerifyCollision.EveryObject:

                    return true;
                    
                    break;
            }

            return false;
        }
        
        #endregion

    }
}
