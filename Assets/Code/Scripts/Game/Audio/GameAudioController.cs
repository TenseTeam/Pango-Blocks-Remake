namespace ProjectPBR.Audio
{
    using UnityEngine;
    using VUDK.Features.Main.AudioSFX;
    using ProjectPBR.GameConfig.Constants;

    public class GameAudioController : AudioEventsControllerBase
    {
        //private AudioClip clip;

        protected override void RegisterAudioEvents()
        {
            // Register audio events here
            //EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, () => AudioManager.PlayStereoAudio(clip));
        }

        protected override void UnregisterAudioEvents()
        {
            // Unregister audio events here
            //EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, () => AudioManager.PlayStereoAudio(clip));
        }
    }
}
