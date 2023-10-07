namespace VUDK.Features.Main.VoiceRecognition
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Windows.Speech;
    using VUDK.Generic.Serializable;

    public sealed class VoiceEventRecognizer : MonoBehaviour
    {
        [SerializeField]
        private SerializableDictionary<string, UnityEvent> _keywordEvents;
        private KeywordRecognizer _keywordRecognizer;

        private void Awake()
        {
            _keywordRecognizer = new KeywordRecognizer(_keywordEvents.Dict.Keys.ToArray());
        }

        private void OnEnable()
        {
            _keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        }

        private void OnDisable()
        {
            _keywordRecognizer.OnPhraseRecognized -= OnPhraseRecognized;
        }

        public void StartRecognition()
        {
            _keywordRecognizer.Start();
#if DEBUG
            Debug.Log("Listening...");
#endif
        }

        public void StopRecognition()
        {
            _keywordRecognizer.Stop();
#if DEBUG
            Debug.Log("Stopped listening.");
#endif
        }

        public void Dispose()
        {
            _keywordRecognizer.Dispose();
        }

        private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
#if DEBUG
            Debug.Log("Reocognized: " + args.text);
#endif
            _keywordEvents[args.text].Invoke();
        }
    }
}