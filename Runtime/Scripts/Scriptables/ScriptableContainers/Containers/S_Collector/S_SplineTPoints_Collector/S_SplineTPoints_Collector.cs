// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolKit.Runtime
{
    [CreateAssetMenu]
    public class S_SplineTPoints_Collector : S_Content_Collector<SplineTPoints>
    {
        [Range(0, 1)] [SerializeField] private float spawnPoint;
        private SplineTPoints CurrentPoint;
    }
}
