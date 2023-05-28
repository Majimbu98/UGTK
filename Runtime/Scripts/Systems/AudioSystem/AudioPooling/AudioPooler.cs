// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Manages the pooling of audio objects.
    /// </summary>
    public class AudioPooler : ObjectPooler
    {
        /// <summary>
        /// Spawns an audio object from the pool.
        /// </summary>
        /// <param name="audio">The audio data to be assigned to the spawned object.</param>
        /// <returns>The spawned audio object.</returns>
        public AudioPoolable SpawnAudio(S_Audio audio)
        {
            // Get the first available poolable object
            IPoolable poolable = GetFirstPooledObject();// Set the action to be performed when the object is despawned
            poolable.SetActionOnDespawn(() => { EndClip(audio); });

            // Spawn the poolable object as a GameObject with a duration based on the audio clip length
            GameObject audioGameObject = SpawnPoolable(poolable, audio.content.clip.length);

            // Get the AudioPoolable component from the spawned GameObject
            AudioPoolable audioPoolable = audioGameObject.GetComponent<AudioPoolable>();
        
            // Initialize the AudioPoolable component with the audio data
            audioPoolable.Initialize(audio);

            return audioPoolable;
        }
        
        /// <summary>
        /// Spawns an audio object from the pool.
        /// </summary>
        /// <param name="audio">The audio data to be assigned to the spawned object.</param>
        /// <returns>The spawned audio object.</returns>
        public AudioPoolable SpawnAudioWithActionAtEnd(S_Audio audio, Action endAction)
        {
            // Get the first available poolable object
            IPoolable poolable = GetFirstPooledObject();// Set the action to be performed when the object is despawned
            poolable.SetActionOnDespawn(endAction);

            // Spawn the poolable object as a GameObject with a duration based on the audio clip length
            GameObject audioGameObject = SpawnPoolable(poolable, audio.content.clip.length);

            // Get the AudioPoolable component from the spawned GameObject
            AudioPoolable audioPoolable = audioGameObject.GetComponent<AudioPoolable>();
        
            // Initialize the AudioPoolable component with the audio data
            audioPoolable.Initialize(audio);

            return audioPoolable;
        }

        /// <summary>
        /// Handles the end of the audio clip based on its properties.
        /// </summary>
        /// <param name="audio">The audio data.</param>
        private void EndClip(S_Audio audio)
        {
            if (audio.content.loop)
            {
                // Invoke the event for repeating loop audio
                EventManager.OnRepeatLoopAudio?.Invoke(audio);
            }
            else
            {
                // Invoke the event for ending audio
                EventManager.OnStopAudio?.Invoke(audio);
            }
        }
    }
}