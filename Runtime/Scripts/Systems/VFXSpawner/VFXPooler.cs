// © 2023 Marcello De Bonis. All rights reserved.

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

        /// <summary>
        /// Spawns a visual effects (VFX) object from the pool.
        /// </summary>
        public void SpawnVFX()
        {
            // Casting the first pooled object to VFXPoolable type
            VFXPoolable poolable = (VFXPoolable)GetFirstPooledObject();
            
            // Spawning the poolable VFX and setting its duration
            SpawnPoolable(poolable, poolable.particleSystem.main.duration);
        }

        #endregion

    }
}
