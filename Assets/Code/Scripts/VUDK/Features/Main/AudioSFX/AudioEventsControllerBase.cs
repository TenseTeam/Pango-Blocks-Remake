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

        /// <summary>
        /// Registers audio events.
        /// </summary>
        protected abstract void RegisterAudioEvents();

        /// <summary>
        /// Unregisters audio events.
        /// </summary>
        protected abstract void UnregisterAudioEvents();
    }
}
