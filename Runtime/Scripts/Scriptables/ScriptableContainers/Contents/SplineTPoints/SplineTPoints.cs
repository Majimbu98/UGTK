// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolKit.Runtime
{
    [System.Serializable]
    public class SplineTPoints : Content<SplineTPoints>
    {
        [Range(0, 1)] [SerializeField] public float firstPoint;
        [Range(0, 1)] [SerializeField] public float secondPoint;
    }
}
