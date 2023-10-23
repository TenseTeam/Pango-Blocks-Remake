namespace VUDK.Generic.Managers.Main
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions.Audio;
    using VUDK.Generic.Serializable;

    public sealed class AudioManager : MonoBehaviour
    {
        [SerializeField, Header("Uncuncurrent AudioSources")]
        [Tooltip("Uncuncurrent audio source for music")]
        private AudioSource _music;
        [field: SerializeField]
        [Tooltip("Uncuncurrent audio source for effects")]
        private AudioSource _source;

        [SerializeField, Header("Concurrent AudioSources")]
        [Tooltip("Concurrent audio sources for multiple effects")]
        private List<AudioSource> _sources;

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
                audio = _sources[0].gameObject.AddComponent<AudioSource>();
                _sources.Add(audio);
            }

            PlayAudio(audio, clip);
        }

        public void PlayUncuncurrentEffectAudio(AudioClip clip, Range<float> pitchVariation = null)
        {
            PlayAudio(_source, clip, pitchVariation);
        }

        /// <summary>
        /// Plays an audio clip.
        /// </summary>
        /// <param name="source">Audio source.</param>
        /// <param name="clip">Audio clip.</param>
        /// <param name="pitchVariation">Pitch variation.</param>
        /// <returns>True if played, false otherwise.</returns>
        private void PlayAudio(AudioSource source, AudioClip clip, Range<float> pitchVariation = null)
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
            foreach (AudioSource effect in _sources)
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