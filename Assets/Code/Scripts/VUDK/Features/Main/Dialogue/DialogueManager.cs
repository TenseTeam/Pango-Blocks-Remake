namespace VUDK.Features.Main.DialogueSystem
{
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VUDK.Features.Main.DialogueSystem.Data;
    using VUDK.Generic.Managers.Main;

    public class DialogueManager : MonoBehaviour
    {
        [SerializeField, Min(0.01f), Header("Sentence")]
        private float _displayLetterTime;

        [SerializeField, Header("Dialogue")]
        private RectTransform _dialoguePanel;
        [SerializeField]
        private Image _speakerImage;
        [SerializeField]
        private TMP_Text _speakerName;
        [SerializeField]
        private TMP_Text _sentenceText;

        private Dialogue _dialogue;
        private Sentence _currentSentence;
        private SpeakerData _currentSpeaker;

        public bool IsDialogueOpen => _dialoguePanel.gameObject.activeSelf;
        public bool IsTalking { get; private set; }

        [Header("Events")]
        public UnityEvent OnEndDialogue;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<Dialogue>(EventKeys.DialogueEvents.OnTriggeredDialouge, StartDialogue);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<Dialogue>(EventKeys.DialogueEvents.OnTriggeredDialouge, StartDialogue);
        }

        public void StartDialogue(Dialogue dialogue)
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.DialogueEvents.OnStartDialogue);
            _dialogue = dialogue;
            _dialoguePanel.gameObject.SetActive(true);
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_dialogue.IsEnded && !IsTalking)
            {
                EndDialogue();
                return;
            }

            StopAllCoroutines();
            if (!IsTalking)
            {
                _currentSentence = _dialogue.NextSentence();
                _currentSpeaker = _dialogue.GetSpeakerForSentence(_currentSentence);
                SetSentenceSpeaker(_currentSpeaker);
                StartCoroutine(TypeSentenceRoutine(_currentSentence));
            }
            else
            {
                IsTalking = false;
                SetCompleteSentence(_currentSentence);
            }
        }

        private void EndDialogue()
        {
            OnEndDialogue?.Invoke();
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.DialogueEvents.OnEndDialogue);
            _dialoguePanel.gameObject.SetActive(false);
            _sentenceText.text = "";
        }

        private IEnumerator TypeSentenceRoutine(Sentence sentence)
        {
            _sentenceText.text = "";
            IsTalking = true;
            foreach (char letter in sentence.Phrase.ToCharArray())
            {
                MainManager.Ins.EventManager.TriggerEvent(EventKeys.DialogueEvents.OnDialougeTypedLetter, _currentSpeaker);
                _sentenceText.text += letter;
                yield return new WaitForSeconds(_displayLetterTime);
            }
            IsTalking = false;
        }

        private void SetSentenceSpeaker(SpeakerData speaker)
        {
            _speakerImage.sprite = speaker.SpeakerImage;
            _speakerName.text = speaker.SpeakerName;
        }

        private void SetCompleteSentence(Sentence sentence)
        {
            SetSentenceSpeaker(_dialogue.GetSpeakerForSentence(sentence));
            _sentenceText.text = sentence.Phrase;
        }
    }
}