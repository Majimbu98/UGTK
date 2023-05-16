// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    [CreateAssetMenu(menuName = "New Custom Scriptable/ New Audio Channel")]
    public class S_AudioChannel : ScriptableContainer<AudioChannel>
    {
        #if UNITY_EDITOR
        float lastVolume;
        bool lastMute;
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

        #endif
    }
}
