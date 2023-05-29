// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // A serializable class that can hold two values of different types.
    [System.Serializable]
    public class BiClass<T, T1> where T : new() where T1: new()
    {
        // The first value of type T.
        [SerializeField] public T firstValue;

        // The second value of type T1.
        [SerializeField] public T1 secondValue;
    }
}