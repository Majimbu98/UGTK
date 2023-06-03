// © 2023 Marcello De Bonis. All rights reserved

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Manages the audio system in the game.
    /// </summary>
    public class AudioSystem : Singleton<AudioSystem>
    {
        #region Variables & Properties

        /// <summary>
        /// The master audio channel.
        /// </summary>
        [SerializeField] public S_AudioChannel master;
        
        /// <summary>
        /// The audio pooler component.
        /// </summary>
        private AudioPooler audioPooler;
        
        /// <summary>
        /// List of currently playing audio clips
        /// </summary>
        private Dictionary<S_Audio, AudioPoolable> clipInExecution = new Dictionary<S_Audio, AudioPoolable>();
        
        /// <summary>
        /// List of currently reproducing audio clusters.
        /// </summary>
        [HideInInspector]
        public List<S_AudioCluster> reproducingCluster;
        
        #endregion

        #region MonoBehaviour

        /// <summary>
        /// Called when the object is enabled.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.OnPlayAudio += SpawnAudio;
            EventManager.OnStopAudio += DestroyAudio;
            EventManager.OnRepeatLoopAudio += SpawnAudio;
            EventManager.OnUpgradeVolume += ChangeVolume;
            EventManager.OnMuteChannel += MuteVolume;
            EventManager.OnDemuteChannel += DemuteVolume;
            EventManager.OnPlayAudioWithActionAtEnd += SpawnAudioWithActionAtEnd;
        }

        /// <summary>
        /// Called when the object is disabled.
        /// </summary>
        void OnDisable()
        {
            EventManager.OnPlayAudio -= SpawnAudio;
            EventManager.OnStopAudio -= DestroyAudio;
            EventManager.OnRepeatLoopAudio -= SpawnAudio;
            EventManager.OnUpgradeVolume -= ChangeVolume;
            EventManager.OnMuteChannel -= MuteVolume;
            EventManager.OnDemuteChannel -= DemuteVolume;
            EventManager.OnPlayAudioWithActionAtEnd -= SpawnAudioWithActionAtEnd;
        }

        /// <summary>
        /// Called when the new script is loaded into the Unity editor.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            audioPooler = GetComponent<AudioPooler>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Spawns an audio clip.
        /// </summary>
        private void SpawnAudio(S_Audio audio)
        {
            if (audio != null && !clipInExecution.ContainsKey(audio))
            {
                clipInExecution.Add(audio, audioPooler.SpawnAudio(audio));
            }
            else
            {
                Debug.Log("Error: Audio is null!!!");
            }
        }

        /// <summary>
        /// Spawns an audio clip with an action at the end.
        /// </summary>
        public void SpawnAudioWithActionAtEnd(S_Audio audio, Action endAction)
        {
            if (audio != null && !clipInExecution.ContainsKey(audio))
            {
                clipInExecution.Add(audio, audioPooler.SpawnAudioWithActionAtEnd(audio, endAction));
            }
            else
            {
                Debug.Log("Error: Audio is null!!!");
            }
        }

        /// <summary>
        /// Destroys an audio clip.
        /// </summary>
        private void DestroyAudio(S_Audio audio)
        {
            if(clipInExecution.ContainsKey(audio))
            {
                AudioPoolable audioPoolable = clipInExecution[audio];
                clipInExecution.Remove(audio);
                IPoolable poolable = (IPoolable)audioPoolable;
                poolable.Despawn();
            }
        }

        /// <summary>
        /// Changes the volume of audio clips based on the provided audio channel.
        /// </summary>
        private void ChangeVolume(S_AudioChannel channel)
        {
            foreach (AudioPoolable audio in clipInExecution.Values)
            {
                if (audio.s_audio.content.channel == channel)
                {
                    audio.ChangeVolume();
                }
            }
        }

        /// <summary>
        /// Mutes the volume of audio clips based on the provided audio channel.
        /// </summary>
        private void MuteVolume(S_AudioChannel channel)
        {
            foreach (AudioPoolable audio in clipInExecution.Values)
            {
                if (audio.s_audio.content.channel == channel)
                {
                    audio.Mute();
                }
            }
        }

        /// <summary>
        /// Demutes the volume of audio clips based on the provided audio channel.
        /// </summary>
        private void DemuteVolume(S_AudioChannel channel)
        {
            foreach (AudioPoolable audio in clipInExecution.Values)
            {
                if (audio.s_audio.content.channel == channel)
                {
                    audio.Demute();
                }
            }
        }

        /// <summary>
        /// Checks if an audio clip is currently being played.
        /// </summary>
        public bool IsAudioReproducing(S_Audio audio)
        {
            foreach (AudioPoolable m_audio in clipInExecution.Values)
            {
                if (m_audio.s_audio == audio)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}