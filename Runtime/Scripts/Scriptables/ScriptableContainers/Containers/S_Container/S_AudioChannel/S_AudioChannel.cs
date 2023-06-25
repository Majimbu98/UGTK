// © 2023 Marcello De Bonis. All rights reserved.

using System.Dynamic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Scriptable object class representing an audio channel.
    // This class inherits from the generic ScriptableContainer class with AudioChannel as the generic parameter.
    // It is used to create custom audio channels in the Unity editor.
    [CreateAssetMenu(menuName = "New Custom Scriptable/Audio/New Audio Channel")]
    public class S_AudioChannel : S_Container<AudioChannel>
    {
        
        float lastVolume;
        bool lastMute;

        // Summary:
        // This method is called when the object is validated in the Unity editor.
        // It compares the current volume and mute settings with the previous values,
        // and applies any necessary changes to the AudioChannel content.
        private void OnValidate()
        {
            if (lastVolume != content.volume)
            {
                lastVolume = content.volume;
                content.SetVolume(lastVolume);
            }

            if (lastMute != content.mute)
            {
                lastMute = content.mute;
                content.ChangeMute();
            }
        }
    }
}