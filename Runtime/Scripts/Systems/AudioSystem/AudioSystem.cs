// © 2023 Marcello De Bonis. All rights reserved

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace UnityGamesToolkit.Runtime
{

    public class AudioSystem : Singleton<AudioSystem>
    {

        // Defines variables and properties
        #region Variables & Properties

        [SerializeField] public S_AudioChannel master;
        private AudioPooler audioPooler;
        private List<AudioPoolable> clipInExecution = new List<AudioPoolable>();

        #endregion

        // Defines MonoBehaviour lifecycle events
        #region MonoBehaviour


        #region Activation/Deactivation

        // Called when the object is enabled
        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.OnStartAudio += SpawnAudio;
            EventManager.OnEndAudio += DestroyAudio;
            EventManager.OnRepeatLoopAudio += SpawnAudio;
            EventManager.OnUpgradeVolume += ChangeVolume;
            EventManager.OnMuteChannel += MuteVolume;
            EventManager.OnDemuteChannel += DemuteVolume;
        }

        // Called when the object is disabled
        void OnDisable()
        {
            EventManager.OnStartAudio -= SpawnAudio;
            EventManager.OnEndAudio -= DestroyAudio;
            EventManager.OnRepeatLoopAudio -= SpawnAudio;
            EventManager.OnUpgradeVolume -= ChangeVolume;
            EventManager.OnMuteChannel -= MuteVolume;
            EventManager.OnDemuteChannel -= DemuteVolume;
        }

        #endregion

        // Called when the new script is loaded into the Unity editor
        protected override void Awake()
        {
            base.Awake();
            audioPooler = GetComponent<AudioPooler>();
        }

        #endregion

        // Defines methods for the new script
        #region Methods

        private void SpawnAudio(S_Audio audio)
        {
            if (audio != null)
            {
                clipInExecution.Add(audioPooler.SpawnAudio(audio));
            }
            else
            {
                Debug.Log("Error: Audio null!!!");
            }
        }

        private void DestroyAudio(S_Audio audio)
        {
            AudioPoolable audioPoolable = GetAudioPoolableFromList(audio);
            if (audioPoolable != null)
            {
                clipInExecution.Remove(audioPoolable);
                IPoolable poolable = (IPoolable)audioPoolable;
                poolable.Despawn();
            }
        }

        private AudioPoolable GetAudioPoolableFromList(S_Audio audio)
        {
            foreach (AudioPoolable poolable in clipInExecution)
            {
                if (poolable.s_audio == audio)
                {
                    return poolable;
                }
            }

            return null;
        }

        private void ChangeVolume(S_AudioChannel channel)
        {
            foreach (AudioPoolable audio in clipInExecution)
            {
                if (audio.s_audio.content.channel == channel)
                {
                    audio.ChangeVolume();
                }
            }
        }

        private void MuteVolume(S_AudioChannel channel)
        {
            foreach (AudioPoolable audio in clipInExecution)
            {
                if (audio.s_audio.content.channel == channel)
                {
                    audio.Mute();
                }
            }
        }

        private void DemuteVolume(S_AudioChannel channel)
        {
            foreach (AudioPoolable audio in clipInExecution)
            {
                if (audio.s_audio.content.channel == channel)
                {
                    audio.Demute();
                }
            }
        }
        
        #endregion

    }
}
