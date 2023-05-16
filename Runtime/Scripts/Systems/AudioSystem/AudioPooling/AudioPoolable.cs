// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{ 
        public class AudioPoolable : MonoBehaviour, IPoolable 
        { 
                // Defines variables and properties
                #region Variables & Properties

                private AudioSource audioSource=new AudioSource();
                private S_AudioChannel audioChannel=null;
                
                [HideInInspector]
                public S_Audio s_audio;
                        
                #endregion
                
                // Defines MonoBehaviour lifecycle events
                #region MonoBehaviour
                

                // Called when the new script is loaded into the Unity editor
                void Awake()
                { 
                        audioSource = GetComponent<AudioSource>();
                }
                
                #endregion
                
                // Defines methods for the new script
                #region Methods

                public void Initialize(S_Audio audio)
                {
                        s_audio = audio;
                        audioSource.clip = audio.content.clip;
                        audioChannel = audio.content.channel;
                        ChangeVolume();
                        audioSource.Play();
                }
                
                public void ChangeVolume()
                {
                        if (!s_audio.content.channel.content.mute)
                        {
                                audioSource.volume = audioChannel.content.volume * AudioSystem.Instance.master.content.volume;    
                        }
                }

                public void Mute()
                {
                        audioSource.volume = 0;
                }

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
                public Action actionOnSpawn { get; set; }
                public Action actionOnDespawn { get; set; }

                #endregion
        }
}
