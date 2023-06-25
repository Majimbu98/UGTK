// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Singleton class for managing coroutines.
    public class Coroutine : Singleton<Coroutine>
    {
        // Summary:
        // Awake is called when the script instance is being loaded.
        // Overrides the base Awake method.
        protected override void Awake()
        {
            base.Awake();
        }
    }
}