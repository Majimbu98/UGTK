// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// ScriptableObject class representing an audio cluster.
    /// </summary>
    /// <remarks>
    /// This class inherits from the generic ScriptableContainer class with Audio as the generic parameter.
    /// It is used to create custom audio objects in the Unity editor.
    /// </remarks>
    [CreateAssetMenu(menuName = "New Custom Scriptable/Audio/New Audio Cluster")]
    public class S_AudioCluster : S_Collector<S_Audio>
    {
        [HideInInspector]
        public int index = 0; // Current index of the audio cluster.

        /// <summary>
        /// Resets the index of the audio cluster to 0.
        /// </summary>
        public void ResetIndex()
        {
            index = 0;
        }

        /// <summary>
        /// Returns the currently playing song in the audio cluster.
        /// </summary>
        /// <returns>The S_Audio object representing the current song.</returns>
        public S_Audio CurrentSong()
        {
            return list[index];
        }

        /// <summary>
        /// Increases the song index of the audio cluster.
        /// </summary>
        public void IncreaseSongIndex()
        {
            index++;
        }

        /// <summary>
        /// Checks if the current song index is within the bounds of the audio cluster.
        /// </summary>
        /// <returns>True if the current song index is less than the total number of songs, false otherwise.</returns>
        public bool ExistCurrentSong()
        {
            return index < list.Count;
        }
    }
}