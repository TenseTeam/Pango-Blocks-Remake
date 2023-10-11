namespace VUDK.Features.Main.DialogueSystem
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.TriggerSystem;

    [RequireComponent(typeof(Collider))]
    public class DialogueTrigger : TriggerEvent
    {
        [SerializeField]
        private Dialogue _dialogue;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            TriggerDialogue();
        }

        public void TriggerDialogue()
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.DialogueEvents.OnTriggeredDialouge, _dialogue);
        }
    }
}
