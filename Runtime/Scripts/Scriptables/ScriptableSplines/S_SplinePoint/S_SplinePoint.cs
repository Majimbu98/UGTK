// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    [CreateAssetMenu(menuName = "New Custom Scriptable/Spline/New Point")]
    public class S_SplinePoint : ScriptableObject
    {
        // Defines variables and properties
        #region Variables & Properties

        [Range(0f, 1f)]
        [SerializeField] public float value;

        #endregion

        // Defines methods for the new script

        #region Methods



        #endregion
    }
}
