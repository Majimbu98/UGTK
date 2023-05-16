// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public class AudioPooler : ObjectPooler
    {
        public AudioPoolable SpawnAudio(S_Audio audio)
        {
            IPoolable poolable = GetFirstPooledObject();
            
            poolable.SetActionOnDespawn(() => { EndClip(audio); });

            GameObject audioGameObject = SpawnPoolable(poolable, audio.content.clip.length);

            AudioPoolable audioPoolable = audioGameObject.GetComponent<AudioPoolable>();
            
            audioPoolable.Initialize(audio);

            return audioPoolable;
        }

        private void EndClip(S_Audio audio)
        {
            if (audio.content.loop)
            {
                EventManager.OnRepeatLoopAudio?.Invoke(audio);
            }
            else
            {
                EventManager.OnEndAudio?.Invoke(audio);
            }
        }
    }
}
