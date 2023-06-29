// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Base abstract class for a scriptable container.
    // This class is a generic class and inherits from ScriptableObject.
    // The generic parameter T must be a type that derives from Content<T> and has a parameterless constructor.
    public abstract class S_Container<T> : ScriptableObject where T : Content<T>
    {
        // Summary:
        // The content of the container.
        public T content;
    }
}