// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Base abstract class for content objects.
    // This class is a generic class and implements the IClonable<T> interface.
    // The generic parameter T must be a type that derives from Content<T> and has a parameterless constructor.
    [System.Serializable]
    public class Content<T> : IClonable<T> where T : Content<T>, new()
    {
        // Summary:
        // Creates a clone of the content object.
        // The method returns a new instance of the generic parameter type T.
        public T Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            formatter.Serialize(memoryStream, this);
            memoryStream.Seek(0, SeekOrigin.Begin);

            T clone = formatter.Deserialize(memoryStream) as T;

            memoryStream.Close();

            return clone;
        }

        public virtual void Init(){}
    }
}