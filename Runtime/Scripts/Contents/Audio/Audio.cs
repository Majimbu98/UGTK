using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

[System.Serializable]
public class Audio : Content<Audio>
{

// Defines variables and properties
#region Variables & Properties

[SerializeField] public AudioClip clip;
[SerializeField] public S_AudioChannel channel;
[SerializeField] public bool loop;

#endregion

}
