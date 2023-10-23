namespace VUDK.Extensions.Audio
{
    using UnityEngine;
    using VUDK.Factories;

    public static class AudioExtension
    {
        public static void PlayClipAtPoint(this AudioClip clip, Vector3 position)
        {
            SFXFactory.Create(clip).PlayClipAtPoint(position);
        }
    }
}
