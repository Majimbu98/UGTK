// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents an audio object that can be pooled.
    /// </summary>
    public class AudioPoolable : MonoBehaviour, IPoolable
    {
        // Defines variables and properties
        #region Variables & Properties

        private AudioSource audioSource;
        private S_AudioChannel audioChannel = null;

        [HideInInspector]
        public S_Audio s_audio;

        #endregion

        // Defines MonoBehaviour lifecycle events
        #region MonoBehaviour

        // Called when the new script is loaded into the Unity editor
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        #endregion

        // Defines methods for the new script
        #region Methods

        /// <summary>
        /// Initializes the audio object with the given audio data.
        /// </summary>
        /// <param name="audio">The audio data to initialize with.</param>
        public void Initialize(S_Audio audio)
        {
            s_audio = audio;
            audioSource.clip = audio.content.clip;
            audioChannel = audio.content.channel;
            ChangeVolume();
            audioSource.Play();
        }

        /// <summary>
        /// Changes the volume of the audio object.
        /// </summary>
        public void ChangeVolume()
        {
            if (!s_audio.content.channel.content.mute)
            {
                audioSource.volume = audioChannel.content.volume * AudioSystem.Instance.master.content.volume;
            }
        }

        /// <summary>
        /// Mutes the audio object.
        /// </summary>
        public void Mute()
        {
            audioSource.volume = 0;
        }

        /// <summary>
        /// Unmutes the audio object and changes the volume.
        /// </summary>
        public void Demute()
        {
            ChangeVolume();
        }

        #endregion

        #region Interface Implementation

        public GameObject self { get; set; }
        public Transform transformObject { get; set; }
        public GameObject parentWhenActivated { get; set; }
        public GameObject parentWhenDeactivated { get; set; }
        public float dieTime { get; set; }
        public Action actionOnDespawn { get; set; }

        #endregion
    }
}