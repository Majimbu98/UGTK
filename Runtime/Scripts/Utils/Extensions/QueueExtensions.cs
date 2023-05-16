// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Provides extension methods for Queue<T> class.
    /// </summary>
    public static class QueueExtensions
    {
        /// <summary>
        /// Converts a Queue<T> to a List<T>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the queue.</typeparam>
        /// <param name="queue">The queue to convert.</param>
        /// <returns>A List<T> containing the elements from the queue.</returns>
        public static List<T> ConvertQueueToList<T>(Queue<T> queue)
        {
            return new List<T>(queue);
        }
    }
}