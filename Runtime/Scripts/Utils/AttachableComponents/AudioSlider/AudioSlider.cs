// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UnityGamesToolkit.Runtime
{
    // Class to handle an audio slider
    public class AudioSlider: MonoBehaviour
    {
        // Variables and properties
        
        [SerializeField] private S_AudioChannel audioChannel; // Audio channel associated with the slider
        [SerializeField] private Slider slider; // Slider for volume control

        // MonoBehaviour lifecycle events

        // Called when the object is enabled
        void OnEnable()
        {
            slider.value = audioChannel.content.volume; // Set the slider value based on the audio channel volume
        }

        // Called when the object is disabled
        void OnDisable()
        {
            audioChannel.content.volume = slider.value;
        }

        // Methods
        // Set the volume of the audio channel based on the slider value
        public void SetVolumeToAudioChannel()
        {
            audioChannel.content.volume = slider.value;
            EventManager.OnUpgradeVolume?.Invoke(audioChannel);
        }
    }
}