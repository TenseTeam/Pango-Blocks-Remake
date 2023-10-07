namespace VUDK.Extensions.Audio.Factory
{
    using UnityEngine;
    using VUDK.Generic.Managers;
    using VUDK.Constants;

    public static class AudioSFXFactory
    {
        public static AudioSFX Create(AudioClip clip)
        {
            GameObject goAud = GameManager.Instance.PoolsManager.Pools[Constants.Pools.AudioSFXPool].Get();

            if (goAud.TryGetComponent(out AudioSFX audioSFX))
                audioSFX.Init(clip);

            return audioSFX;
        }
    }
}
