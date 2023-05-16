// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{ 
    [System.Serializable]
    public class AudioChannel : Content<AudioChannel>
    {
        [SerializeField] [Range(0f, 1f)] public float volume;
        [SerializeField] private S_AudioChannel self;
        
        public float Percentage()
        {
            return volume / 1;
        }
        
        public void IncreaseVolumeOf(float volumeToIncrease = 0.1f)
        {
            if (volume + volumeToIncrease > 1)
            {
                volume += volumeToIncrease;
            }
            
            EventManager.OnUpgradeVolume?.Invoke(self);
        }
        
        public void DecreaseVolumeOf(float volumeToDecrease = 0.1f)
        {
            if (volume - volumeToDecrease < 0)
            {
                volume -= volumeToDecrease;
            }
            
            EventManager.OnUpgradeVolume?.Invoke(self);
        }
    }
}
