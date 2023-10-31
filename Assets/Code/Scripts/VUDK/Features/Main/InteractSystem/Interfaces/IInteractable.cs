namespace VUDK.Features.Main.InteractSystem.Interfaces
{
    using UnityEngine;

    public interface IInteractable
    {
        /// <summary>
        /// Interacts with this object.
        /// </summary>
        /// <param name="Interactor">Interactor GameObject.</param>
        public void Interact(InteractorBase interactor);
    }
}