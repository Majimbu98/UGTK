// © 2023 Marcello De Bonis. All rights reserved.

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Interface for cloning objects.
    /// </summary>
    /// <typeparam name="T">The type of object to clone.</typeparam>
    public interface IClonable<T>
    {
        /// <summary>
        /// Creates a clone of the object.
        /// </summary>
        /// <returns>A clone of the object.</returns>
        T Clone();
    }
}