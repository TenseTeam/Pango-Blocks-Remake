namespace VUDK.Generic.Managers.Main
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions.Audio;
    using VUDK.Generic.Serializable;

    public abstract class AudioManager : MonoBehaviour
    {
        [SerializeField, Header("Uncuncurrent AudioSources")]
        protected AudioSource Music;
        [field: SerializeField]
        protected AudioSource StereoSourceEffect;

        [SerializeField, Header("Concurrent AudioSources")]
        protected List<AudioSource> StereoSourceEffects;

        protected abstract void OnEnable();

        protected abstract void OnDisable();

        public void PlaySpatialAudio(AudioClip clip, Vector3 position)
        {
            AudioExtension.PlayClipAtPoint(clip, position);
        }

        public void PlayStereoAudio(AudioClip clip, bool isConcurrent = false)
        {
            if(isConcurrent)
                PlayConcurrentEffectAudio(clip);
            else
                PlayUncuncurrentEffectAudio(clip);
        }

        public void PlayConcurrentEffectAudio(AudioClip clip)
        {
            AudioSource audio;

            if (TryFindFreeAudioSource(out AudioSource foundAudio))
                audio = foundAudio;
            else
            {
                audio = StereoSourceEffects[0].gameObject.AddComponent<AudioSource>();
                StereoSourceEffects.Add(audio);
            }

            PlayAudio(audio, clip);
        }

        public void PlayUncuncurrentEffectAudio(AudioClip clip, Range<float> pitchVariation = null)
        {
            PlayAudio(StereoSourceEffect, clip, pitchVariation);
        }

        /// <summary>
        /// Plays an audio clip.
        /// </summary>
        /// <param name="source">Audio source.</param>
        /// <param name="clip">Audio clip.</param>
        /// <param name="pitchVariation">Pitch variation.</param>
        /// <returns>True if played, false otherwise.</returns>
        protected void PlayAudio(AudioSource source, AudioClip clip, Range<float> pitchVariation = null)
        {
            if(pitchVariation != null)
                source.pitch = pitchVariation.Random();

            source.clip = clip;
            source.Play();
        }

        /// <summary>
        /// Tries to find free audio source.
        /// </summary>
        /// <param name="audio">Found audio source.</param>
        /// <returns>True if found, false otherwise.</returns>
        private bool TryFindFreeAudioSource(out AudioSource audio)
        {
            foreach (AudioSource effect in StereoSourceEffects)
            {
                if (!effect.isPlaying)
                {
                    audio = effect;
                    return true;
                }
            }

            audio = null;
            return false;
        }
    }
}