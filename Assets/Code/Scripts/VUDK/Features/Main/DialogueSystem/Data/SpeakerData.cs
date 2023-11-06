namespace VUDK.Features.Main.DialogueSystem.Data
{
    using UnityEngine;
    using VUDK.Generic.Serializable;

    [CreateAssetMenu(menuName = "VUDK/Dialogue/Speaker")]
    public class SpeakerData : ScriptableObject
    {
        public string SpeakerName;
        public Sprite SpeakerImage;

        //[Header("Audio")]
        //public AudioClip SpeakerLetterAudio;
        //public Range<float> PitchVariation;
    }
}