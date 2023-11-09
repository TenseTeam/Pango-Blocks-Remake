namespace VUDK.Extensions.Audio
{
    using UnityEngine;
    using VUDK.Factories;

    public static class AudioExtension
    {
        /// <summary>
        /// Play clip at point in space.
        /// </summary>
        /// <param name="clip"><see cref="AudioClip"/> to play.</param>
        /// <param name="position">Position in space from where to play it.</param>
        public static void PlayClipAtPoint(this AudioClip clip, Vector3 position)
        {
            SFXFactory.Create(clip).PlayClipAtPoint(position);
        }
    }
}
