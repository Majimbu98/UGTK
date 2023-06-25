// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents a pooler for visual effects (VFX) objects.
    /// </summary>
    public class VFXPooler : ObjectPooler<VFXPoolable>
    {
        // Defines methods for the new script
        #region Methods
        
        [Header("VFX Pooler Variables")]
        [SerializeField] private float deelaySpawn;

        private bool spawnConsecutively=false;
        
        /// <summary>
        /// Spawns a visual effects (VFX) object from the pool.
        /// </summary>
        public IPoolable SpawnVFX(bool useHisDieTime)
        {
            // Casting the first pooled object to VFXPoolable type
            VFXPoolable poolable = (VFXPoolable)GetFirstPooledObject();
            
            if (useHisDieTime)
            {
                // Spawning the poolable VFX and setting its duration
                SpawnPoolable(poolable, poolable.particleSystem.main.duration);
            }
            else
            {
                SpawnPoolable(poolable);
            }
            
            //Reproduce poolable VFX
            poolable.PlayParticleSystem();

            return poolable;
        }

        public void PlaySpawnVFXConsecutively()
        {
            spawnConsecutively = true;
            StartCoroutine(SpawnConsecutivelyFunction());
        }

        public void StopSpawnVFXConsecutively()
        {
            spawnConsecutively = false;
        }
        
        private IEnumerator SpawnConsecutivelyFunction()
        {
            while (spawnConsecutively)
            {
                SpawnVFX(true);
                yield return new WaitForSeconds(deelaySpawn);
            }
        }
        
        #endregion

    }
}
