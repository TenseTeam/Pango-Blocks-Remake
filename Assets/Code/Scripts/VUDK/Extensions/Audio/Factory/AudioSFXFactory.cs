namespace VUDK.Extensions.Audio.Factory
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Config.Constants;

    public static class AudioSFXFactory
    {
        public static AudioSFX Create(AudioClip clip)
        {
            GameObject goAud = MainManager.Ins.GameManager.PoolsManager.Pools[Constants.Pools.AudioSFXPool].Get();

            if (goAud.TryGetComponent(out AudioSFX audioSFX))
                audioSFX.Init(clip);

            return audioSFX;
        }
    }
}
