// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // Summary:
    // Static class for managing events in the game.
    public static class EventManager
    {
        // Summary:
        // Defines variables and properties for event actions.

        #region Variables & Properties

        #region AudioAction

        // Summary:
        // Event action invoked when an audio channel's volume is upgraded.
        public static Action<S_AudioChannel> OnUpgradeVolume;

        // Summary:
        // Event action invoked when an audio channel is muted.
        public static Action<S_AudioChannel> OnMuteChannel;

        // Summary:
        // Event action invoked when an audio channel is demuted.
        public static Action<S_AudioChannel> OnDemuteChannel;

        // Summary:
        // Event action invoked when an audio starts playing.
        public static Action<S_Audio> OnStartAudio;

        // Summary:
        // Event action invoked when an audio ends.
        public static Action<S_Audio> OnEndAudio;

        // Summary:
        // Event action invoked when an audio repeats its loop.
        public static Action<S_Audio> OnRepeatLoopAudio;

        #endregion

        #endregion

        // Summary:
        // Initializes the EventManager.
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Application.quitting += Shutdown;
        }

        // Summary:
        // Shuts down the EventManager.
        private static void Shutdown()
        {
            // Add shutdown logic here
        }

    }

}