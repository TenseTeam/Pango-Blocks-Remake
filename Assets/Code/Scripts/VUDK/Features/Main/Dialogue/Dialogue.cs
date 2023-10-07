namespace VUDK.Features.Main.DialogueSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Main.DialogueSystem.Data;

    [System.Serializable]
    public struct Sentence
    {
        public int SpeakerId;
        [TextArea(3, 10)]
        public string Phrase;
    }

    [System.Serializable]
    public class Dialogue
    {
        [SerializeField, Header("Speakers")]
        private List<SpeakerData> _speakers;

        [SerializeField, Header("Sentences")]
        private List<Sentence> _sentences;

        private int _index = 0;

        public bool IsEnded => _index == _sentences.Count;

        public Sentence NextSentence()
        {
            return _sentences[_index++];
        }

        public Sentence PreviousSentence()
        {
            return _sentences[_index--];
        }

        public Sentence CurrentSentence()
        {
            return _sentences[_index];
        }

        public SpeakerData GetSpeakerForSentence(Sentence sentence)
        {
            return _speakers[sentence.SpeakerId];
        }
    }
}