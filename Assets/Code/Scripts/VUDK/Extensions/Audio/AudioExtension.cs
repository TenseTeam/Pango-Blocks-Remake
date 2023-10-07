namespace VUDK.Extensions.Audio
{
    using UnityEngine;
    using VUDK.Extensions.Audio.Factory;

    public static class AudioExtension
    {
        public static void PlayClipAtPoint(this AudioClip clip, Vector3 position)
        {
            AudioSFXFactory.Create(clip).PlayClipAtPoint(position);
        }
    }
}
