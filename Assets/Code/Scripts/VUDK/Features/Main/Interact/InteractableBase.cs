namespace VUDK.Features.Main.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem.Interfaces;

    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public abstract void Interact(InteractorBase interactor);

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractorBase interactor))
                interactor.SetIneractable(this);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out InteractorBase interactor))
                interactor.SetIneractable(null);
        }
    }
}
