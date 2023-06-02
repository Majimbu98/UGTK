// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Static class for managing events in the game.
    /// </summary>
    public static class EventManager
    {
        #region Variables & Properties

        #region AudioEvents

        #region Volumes

        /// <summary>
        /// Event action invoked when an audio channel's volume is upgraded.
        /// </summary>
        public static Action<S_AudioChannel> OnUpgradeVolume;

        /// <summary>
        /// Event action invoked when an audio channel is muted.
        /// </summary>
        public static Action<S_AudioChannel> OnMuteChannel;

        /// <summary>
        /// Event action invoked when an audio channel is demuted.
        /// </summary>
        public static Action<S_AudioChannel> OnDemuteChannel;

        #endregion

        #region Play/StopSounds

        /// <summary>
        /// Event action invoked when an audio starts playing.
        /// </summary>
        public static Action<S_Audio> OnPlayAudio;

        /// <summary>
        /// Event action invoked when an audio starts playing with an action to be executed at the end.
        /// </summary>
        public static Action<S_Audio, Action> OnPlayAudioWithActionAtEnd;

        /// <summary>
        /// Event action invoked when an audio stops playing.
        /// </summary>
        public static Action<S_Audio> OnStopAudio;

        /// <summary>
        /// Event action invoked when an audio repeats its loop.
        /// </summary>
        public static Action<S_Audio> OnRepeatLoopAudio;

        #region AudioCluster

        /// <summary>
        /// Event action invoked when an audio cluster starts playing.
        /// </summary>
        public static Action<S_AudioCluster> OnPlayAudioCluster;

        /// <summary>
        /// Event action invoked when the next audio cluster song starts playing.
        /// </summary>
        public static Action<S_AudioCluster> OnNextAudioCluster;

        /// <summary>
        /// Event action invoked when an audio cluster stops playing.
        /// </summary>
        public static Action<S_AudioCluster> OnStopAudioCluster;

        #endregion

        #endregion

        #endregion

        #region VibrationEvents

        /// <summary>
        /// Event action invoked when a vibration is activated.
        /// </summary>
        public static Action<S_Vibration> OnActiveVibration;

        /// <summary>
        /// Event action invoked when a vibration is deactivated.
        /// </summary>
        public static Action<S_Vibration> OnDeactiveVibration;

        #endregion

        #endregion

        #region Methods

        #region Initialization/Deleting

        /// <summary>
        /// Initializes the EventManager.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Application.quitting += Shutdown;
            OnPlayAudioCluster += PlayAudioCluster;
            OnNextAudioCluster += NextAudioCluster;
            OnStopAudioCluster += StopAudioCluster;

            InitVariables();
        }

        /// <summary>
        /// Initializes the variables used by the EventManager.
        /// </summary>
        private static void InitVariables()
        {
            
        }

        /// <summary>
        /// Shuts down the EventManager.
        /// </summary>
        private static void Shutdown()
        {
            OnPlayAudioCluster -= PlayAudioCluster;
            OnNextAudioCluster -= NextAudioCluster;
            OnStopAudioCluster -= StopAudioCluster;

            ClearVariables();
        }

        /// <summary>
        /// Clears the variables used by the EventManager.
        /// </summary>
        private static void ClearVariables()
        {
            
        }

        #endregion

        #region AudioClusterMethods

        /// <summary>
        /// Starts playing an audio cluster.
        /// </summary>
        /// <param name="audioCluster">The audio cluster to play.</param>
        private static void PlayAudioCluster(S_AudioCluster audioCluster)
        {
            audioCluster.ResetIndex();
            OnPlayAudioWithActionAtEnd?.Invoke(audioCluster.CurrentSong(),
                () => { OnNextAudioCluster?.Invoke(audioCluster); });
            AudioSystem.reproducingCluster.Add(audioCluster);
        }

        /// <summary>
        /// Plays the next song in the audio cluster.
        /// </summary>
        /// <param name="audioCluster">The audio cluster to play.</param>
        private static void NextAudioCluster(S_AudioCluster audioCluster)
        {
            if (AudioSystem.reproducingCluster.Contains(audioCluster))
            {
                if (audioCluster.CurrentSong().content.loop)
                {
                    OnPlayAudioWithActionAtEnd?.Invoke(audioCluster.CurrentSong(),
                        () => { OnNextAudioCluster?.Invoke(audioCluster); });
                }
                else
                {
                    OnStopAudio?.Invoke(audioCluster.CurrentSong());
                    audioCluster.IncreaseSongIndex();
                    if (audioCluster.ExistCurrentSong())
                    {
                        OnPlayAudioWithActionAtEnd?.Invoke(audioCluster.CurrentSong(),
                            () => { OnNextAudioCluster?.Invoke(audioCluster); });
                    }
                    else
                    {
                        OnStopAudioCluster?.Invoke(audioCluster);
                    }
                }
            }
        }

        /// <summary>
        /// Stops playing an audio cluster.
        /// </summary>
        /// <param name="audioCluster">The audio cluster to stop.</param>
        private static void StopAudioCluster(S_AudioCluster audioCluster)
        {
            AudioSystem.reproducingCluster.Remove(audioCluster);
            OnStopAudio?.Invoke(audioCluster.CurrentSong());
            audioCluster.ResetIndex();
        }

        #endregion

        #endregion
    }
}