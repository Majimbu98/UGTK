// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;

namespace UnityGamesToolkit.Runtime
{
    public static class QueueExtensions
    {
        public static List<T> ConvertQueueToList<T>(Queue<T> queue) 
        { 
            return new List<T>(queue);
        }
    }
}
