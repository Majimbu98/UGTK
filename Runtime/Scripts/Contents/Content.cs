// © 2023 Marcello De Bonis. All rights reserved.

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Base abstract class for content objects.
    // This class is a generic class and implements the IClonable<T> interface.
    // The generic parameter T must be a type that derives from Content<T> and has a parameterless constructor.
    [System.Serializable]
    public abstract class Content<T> : IClonable<T> where T : Content<T>, new()
    {
        // Summary:
        // Creates a clone of the content object.
        // The method returns a new instance of the generic parameter type T.
        public virtual T Clone()
        {
            return new T();
        }
    }
}