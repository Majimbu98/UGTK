// © 2023 Marcello De Bonis. All rights reserved.

using System;
using Unity.Collections;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    [CreateAssetMenu(menuName = "New Custom Scriptable/Spline/New Point")]
    public class S_SplinePoint : ScriptableObject
    {
        // Defines variables and properties
        #region Variables & Properties

        [Range(0f, 1f)]
        [SerializeField]
        public float value;

        private float valueLastFrame;
        
        [ReadOnly]
        [SerializeField] public Vector3 position;

        [ReadOnly]
        [SerializeField] public S_Spline splineReference;

        #endregion

        // Defines methods for the new script

        #region Methods

        private void OnValidate()
        {
            if (value != valueLastFrame)
            {
              //  position= splineReference.
            }
        }

        public void UpgradeValue(float _value)
        {
            value = _value;
        }

        #endregion
    }
}
