// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public static class EventManager
    {
        // Defines variables and properties
        #region Variables & Properties

        #region AudioAction

        public static Action<S_AudioChannel> OnUpgradeVolume;
        public static Action<S_AudioChannel> OnMuteChannel;
        public static Action<S_AudioChannel> OnDemuteChannel;
        
        public static Action<S_Audio> OnStartAudio;
        public static Action<S_Audio> OnEndAudio;
        
        public static Action<S_Audio> OnRepeatLoopAudio;

        #endregion

        #endregion

        // Defines methods for the new script

        #region Methods

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Application.quitting += Shutdown;
        }

        private static void Shutdown()
        {
            
        }

        #endregion
    }

}