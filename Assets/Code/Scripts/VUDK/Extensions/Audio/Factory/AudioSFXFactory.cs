namespace VUDK.Extensions.Audio.Factory
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;

    public static class AudioSFXFactory
    {
        public static AudioSFX Create(AudioClip clip)
        {
            GameObject goAud = MainManager.Ins.PoolsManager.Pools[PoolKeys.AudioSFX].Get();

            if (goAud.TryGetComponent(out AudioSFX audioSFX))
                audioSFX.Init(clip);

            return audioSFX;
        }
    }
}
