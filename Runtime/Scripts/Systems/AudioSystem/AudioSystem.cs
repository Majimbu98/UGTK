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
        public AudioPooler audioPooler;
        
        /// <summary>
        /// List of currently playing audio clips
        /// </summary>
        public Dictionary<S_Audio, AudioPoolable> clipInExecution = new Dictionary<S_Audio, AudioPoolable>();
        
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
            EventManager.OnMuteAudio += MuteAudio;
            EventManager.OnDeMuteAudio += DeMuteAudio;
            EventManager.OnMuteCluster += MuteAudioCluster;
            EventManager.OnDeMuteCluster += DeMuteAudioCluster;
            EventManager.OnPlayAudioWithActionAtEnd += SpawnAudioWithActionAtEnd;
            
            EventManager.OnPlayAudioCluster += PlayAudioCluster;
            EventManager.OnStopAudioCluster += StopAudioCluster;
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
            EventManager.OnMuteAudio -= MuteAudio;
            EventManager.OnDeMuteAudio -= DeMuteAudio;
            EventManager.OnMuteCluster -= MuteAudioCluster;
            EventManager.OnDeMuteCluster -= DeMuteAudioCluster;
            EventManager.OnPlayAudioWithActionAtEnd -= SpawnAudioWithActionAtEnd;
            
            EventManager.OnPlayAudioCluster -= PlayAudioCluster;
            EventManager.OnStopAudioCluster -= StopAudioCluster;
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

        #region Normal Audio

        /// <summary>
        /// Spawns an audio clip.
        /// </summary>
        private void SpawnAudio(S_Audio audio)
        {
            if (audio != null)
            {
                if (clipInExecution.ContainsKey(audio))
                {
                    clipInExecution.Remove(audio);
                }
                
                clipInExecution.Add(audio, audioPooler.SpawnAudio(audio));
            }
            else
            {
                Debug.Log("Error: "+ typeof(S_Audio).ToString()+" cannot be null");
                //Debug.LogError("Error: "+ typeof(S_Audio).ToString()+" cannot be null");
                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        /// <summary>
        /// Spawns an audio clip with an action at the end.
        /// </summary>
        public void SpawnAudioWithActionAtEnd(S_Audio audio, Action endAction)
        {
            if (audio != null)
            {
                clipInExecution.Add(audio, audioPooler.SpawnAudioWithActionAtEnd(audio, endAction));
            }
            else
            {
                Debug.Log("Error: "+ typeof(S_Audio).ToString()+" cannot be null");
                //Debug.LogError("Error: "+ typeof(S_Audio).ToString()+" cannot be null");
                //UnityEditor.EditorApplication.isPlaying = false;
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
                poolable.SetActionOnDespawn(()=>{});
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
                if (audio.s_audio.content.channel == channel || master == channel)
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
        
        #region ClusterAudio
        
        /// <summary>
        /// Starts playing an audio cluster.
        /// </summary>
        /// <param name="audioCluster">The audio cluster to play.</param
        private void PlayAudioCluster(S_AudioCluster audioCluster)
        {
            audioCluster.ResetIndex();
            S_Audio audio = audioCluster.CurrentSong();
            EventManager.OnPlayAudioWithActionAtEnd?.Invoke(audio, () => { NextAudioCluster(audioCluster); }); 
            reproducingCluster.Add(audioCluster);
        }
        
        
        /// <summary>
        /// Plays the next song in the audio cluster.
        /// </summary>
        /// <param name="audioCluster">The audio cluster to play.</param>
        private void NextAudioCluster(S_AudioCluster audioCluster)
        {
            if (reproducingCluster.Contains(audioCluster))
            {
                S_Audio audio = audioCluster.CurrentSong();
                
                Debug.Log(audio.name);
                
                if (audioCluster.CurrentSong().content.loop)
                {
                    EventManager.OnRepeatLoopAudio?.Invoke(audio);
                }
                else
                {
                    audio = audioCluster.CurrentSong();
                    EventManager.OnStopAudio?.Invoke(audio);
                    audioCluster.IncreaseSongIndex();
                    if (audioCluster.ExistCurrentSong())
                    {
                        audio = audioCluster.CurrentSong();
                        EventManager.OnPlayAudioWithActionAtEnd?.Invoke(audio, () => { NextAudioCluster(audioCluster); });
                    }
                    else
                    {
                        EventManager.OnStopAudioCluster?.Invoke(audioCluster);
                    }
                }
            }
        }

        /// <summary>
        /// Stops playing an audio cluster.
        /// </summary>
        /// <param name="audioCluster">The audio cluster to stop.</param>
        private void StopAudioCluster(S_AudioCluster audioCluster)
        {
            reproducingCluster.Remove(audioCluster);
            audioCluster.ResetIndex();
        }

        private void MuteAudio(S_Audio _audio)
        {
            if (clipInExecution.ContainsKey(_audio))
            {
                clipInExecution[_audio].Mute();
            }
            else
            {
                ConsoleUtility.LogColored("Error, the ScriptableAudio doesn't exists \n You Called by", Color.blue);
            }
        }
        
        private void DeMuteAudio(S_Audio _audio)
        {
            if (clipInExecution.ContainsKey(_audio))
            {
                clipInExecution[_audio].Demute();
            }
            else
            {
                ConsoleUtility.LogColored("Error, the ScriptableAudio doesn't exists", Color.blue);
            }
        }
        
        private void MuteAudioCluster(S_AudioCluster cluster)
        {
            if (reproducingCluster.Contains(cluster))
            {
                MuteAudio(cluster.CurrentSong());
            }
            else
            {
                ConsoleUtility.LogColored("Error, the ScriptableAudio doesn't exists", Color.blue);
            }
        }
        
        private void DeMuteAudioCluster(S_AudioCluster cluster)
        {
            if (reproducingCluster.Contains(cluster))
            {
                DeMuteAudio(cluster.CurrentSong());
            }
            else
            {
                ConsoleUtility.LogColored("Error, the ScriptableAudio doesn't exists", Color.blue);
            }
        }
        
        #endregion

        #endregion
    }
}