// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{ 
    // Summary:
    // Serializable class representing an audio channel.
    // This class derives from the generic Content class with AudioChannel as the generic parameter.
    [System.Serializable]
    public class AudioChannel : Content<AudioChannel>
    {
        // Summary:
        // Reference to the S_AudioChannel scriptable object.
        [SerializeField] private S_AudioChannel self;

        // Summary:
        // The volume of the audio channel, ranged between 0 and 1.
        [SerializeField] [Range(0f, 1f)] public float volume;

        // Summary:
        // Indicates whether the audio channel is muted.
        [SerializeField] public bool mute;

        // Summary:
        // Calculates the volume percentage based on the current volume value.
        // Returns the volume percentage.
        public float VolumePercentage()
        {
            return volume / 1;
        }

        // Summary:
        // Toggles the mute state of the audio channel.
        // Invokes the ChangeMute method.
        public void PressMuteButton()
        {
            mute = !mute;
            ChangeMute();
        }
        
        // Summary:
        // Changes the mute state of the audio channel.
        // Invokes the Mute or Demute method based on the mute state.
        public void ChangeMute()
        {
            if (mute)
            {
                Mute();
            }
            else
            {
                Demute();
            }
        }
        
        // Summary:
        // Mutes the audio channel.
        // Invokes the OnMuteChannel event.
        public void Mute()
        {
            EventManager.OnMuteChannel?.Invoke(self);
        }

        // Summary:
        // Demutes the audio channel.
        // Invokes the OnDemuteChannel event.
        public void Demute()
        {
            EventManager.OnDemuteChannel?.Invoke(self);
        }

        // Summary:
        // Sets the volume of the audio channel to the specified value.
        // Invokes the OnUpgradeVolume event.
        public void SetVolume(float _volume)
        {
            volume = _volume;
            EventManager.OnUpgradeVolume?.Invoke(self);
        }
        
        // Summary:
        // Increases the volume of the audio channel by the specified amount.
        // The volume increase is limited to stay within the range of 0 to 1.
        public void IncreaseVolumeOf(float volumeToIncrease = 0.1f)
        {
            if (volume + volumeToIncrease < 1)
            {
                SetVolume(volume + volumeToIncrease);
            }
        }
        
        // Summary:
        // Decreases the volume of the audio channel by the specified amount.
        // The volume decrease is limited to stay within the range of 0 to 1.
        public void DecreaseVolumeOf(float volumeToDecrease = 0.1f)
        {
            if (volume - volumeToDecrease > 0)
            {
                SetVolume(volume - volumeToDecrease);
            }
        }
    }
}