namespace VUDK.Features.Main.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem.Interfaces;

    public abstract class InteractorBase : MonoBehaviour
    {
        public IInteractable Interactable { get; protected set; }

        public virtual void TriggerInteractable()
        {
            if(Interactable == null) return;

            Interactable.Interact(this);
        }

        public virtual void SetIneractable(IInteractable interactable)
        {
            Interactable = interactable;
        }
    }
}
