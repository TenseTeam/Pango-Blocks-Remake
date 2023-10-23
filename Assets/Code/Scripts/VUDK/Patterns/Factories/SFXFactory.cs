namespace VUDK.Factories
{
    using UnityEngine;
    using VUDK.Features.AudioSFX;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;

    public static class SFXFactory
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
