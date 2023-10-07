namespace VUDK.Generic.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.EventsSystem;
    using VUDK.Features.Main.InputSysten.MobileInputs;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Patterns.Singleton;

    [DefaultExecutionOrder(-50)]
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField, Header("Event Manager")]
        public EventManager EventManager { get; private set; }

        [field: SerializeField, Header("Pooling")]
        public PoolsManager PoolsManager { get; private set; }

        [field: SerializeField, Header("Mobile Inputs")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        //[field: SerializeField, Header("Dialogue")]
        //public DialogueManager DialogueManager { get; private set; }
        
        //[field: SerializeField, Header("Voice Recognizer")]
        //public VoiceEventRecognizer VoiceEventRecognizer { get; private set; }
    }
}