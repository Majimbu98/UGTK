// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{

    public class ObjectPoolable : MonoBehaviour //IPoolable
    {
        #region Variables & Properties

        private GameObject parentWhenDeactivated;
        private GameObject parentWhenActivated;

        [SerializeField]

        #endregion

        #region MonoBehaviour

        public void AttachToActivatedParent()
        {
            gameObject.transform.parent = parentWhenActivated.transform;
        }

        public void AttachToDeactivatedParent()
        {
            gameObject.transform.parent = parentWhenDeactivated.transform;
        }

        #endregion

        #region Methods

        public void SetParents(GameObject parentDeactivation, GameObject parentActivation)
        {
            parentWhenDeactivated = parentDeactivation;
            parentWhenActivated = parentActivation;
        }



        public void StartDeactiveAfterTime(float dieTime)
        {
            StartCoroutine(DeactiveAfterTime(dieTime));
        }

        private IEnumerator DeactiveAfterTime(float dieTime)
        {
            float time = 0f;
            while (time < dieTime)
            {
                time += Time.deltaTime;
                yield return null;
            }

            //NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            AttachToDeactivatedParent();

        }

        public virtual void OnSpawn()
        {

        }

        #endregion

    }

}
