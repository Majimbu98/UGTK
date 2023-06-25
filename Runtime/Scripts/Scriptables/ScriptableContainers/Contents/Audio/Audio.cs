// © 2023 Marcello De Bonis. All rights reserved

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
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
}
