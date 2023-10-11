namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions
{
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions.Interfaces;
    using UnityEngine.EventSystems;

    public class TouchButtonInput : InputTouchBase, ITouchHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            OnInputPerformed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnInputCancelled?.Invoke();
        }
    }
}
