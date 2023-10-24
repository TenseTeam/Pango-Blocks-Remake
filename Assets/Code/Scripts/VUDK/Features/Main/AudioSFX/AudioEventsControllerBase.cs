namespace VUDK.Features.Main.AudioSFX
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public abstract class AudioEventsControllerBase : MonoBehaviour
    {
        protected AudioManager AudioManager => MainManager.Ins.AudioManager;
        protected EventManager EventManager => MainManager.Ins.EventManager;

        private void OnEnable() => RegisterAudioEvents();

        private void OnDisable() => UnregisterAudioEvents();

        protected abstract void RegisterAudioEvents();

        protected abstract void UnregisterAudioEvents();
    }
}
