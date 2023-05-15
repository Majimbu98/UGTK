// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public abstract class ScriptableContainer<T> : ScriptableObject where T : Content<T>, new()
    {
        //[Serializable]
        public T content;
    }
}