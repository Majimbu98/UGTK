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

        public static Action<S_Audio> OnMuteAudio;

        public static Action<S_Audio> OnDeMuteAudio;

        public static Action<S_AudioCluster> OnMuteCluster;
        
        public static Action<S_AudioCluster> OnDeMuteCluster;

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
        /// Event action invoked when an audio cluster stops playing.
        /// </summary>
        public static Action<S_AudioCluster> OnStopAudioCluster;

        #endregion

        #endregion

        #endregion

        #region Mobile

        #region Notification
        //Title, Messagge, deelay from when it could arrives
        public static Action<string, string, float> OnNotifyAppear;

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

        #region Project Settings

        public static Action<S_Language> OnSetNewLanguage;

        #endregion

        #region External Events

        public static Action<string> OnOpenLink;

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
            InitVariables();
        }

        /// <summary>
        /// Initializes the variables used by the EventManager.
        /// </summary>
        private static void InitVariables()
        {
            OnOpenLink += OpenLink;
        }

        /// <summary>
        /// Shuts down the EventManager.
        /// </summary>
        private static void Shutdown()
        {
            ClearVariables();
        }

        /// <summary>
        /// Clears the variables used by the EventManager.
        /// </summary>
        private static void ClearVariables()
        {
            OnOpenLink -= OpenLink;
        }

        #endregion

        private static void OpenLink(string url)
        {
            Application.OpenURL(url);
        }

        #endregion
    }
}