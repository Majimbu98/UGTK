// © 2023 Marcello De Bonis. All rights reserved.

namespace UnityGamesToolkit.Runtime
{
    [System.Serializable]
    public abstract class Content<T> : IClonable<T> where T : Content<T>, new()
    {
        public virtual T Clone()
        {
            return new T();
        }
    }
}