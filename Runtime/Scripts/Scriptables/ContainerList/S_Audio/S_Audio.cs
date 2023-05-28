// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Scriptable object class representing an audio.
    // This class inherits from the generic ScriptableContainer class with Audio as the generic parameter.
    // It is used to create custom audio objects in the Unity editor.
    [CreateAssetMenu(menuName = "New Custom Scriptable/Audio/New Audio")]
    public class S_Audio : ScriptableContainer<Audio>
    {

    }
}