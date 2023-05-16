// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{ 
    [System.Serializable]
    public class AudioChannel : Content<AudioChannel>
    {
        [SerializeField] private S_AudioChannel self;
        [SerializeField] [Range(0f, 1f)] public float volume;
        [SerializeField] public bool mute;
        
        public float VolumePercentage()
        {
            return volume / 1;
        }

        public void PressMuteButton()
        {
            mute = !mute;
            ChangeMute();
        }
        
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
        
        public void Mute()
        {
            EventManager.OnMuteChannel?.Invoke(self);
        }

        public void Demute()
        {
            EventManager.OnDemuteChannel?.Invoke(self);
        }

        public void SetVolume(float _volume)
        {
            volume = _volume;
            EventManager.OnUpgradeVolume?.Invoke(self);
        }
        
        public void IncreaseVolumeOf(float volumeToIncrease = 0.1f)
        {
            if (volume + volumeToIncrease < 1)
            {
                SetVolume(volume + volumeToIncrease);
            }
        }
        
        public void DecreaseVolumeOf(float volumeToDecrease = 0.1f)
        {
            if (volume - volumeToDecrease > 0)
            {
                SetVolume(volume - volumeToDecrease);
            }
        }
    }
}
