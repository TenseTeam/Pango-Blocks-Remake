namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Extensions.Vectors;
    using VUDK.Features.Main.InputSystem;

    public class SwipeInput : InputTouchBase
    {
        [SerializeField, Min(0.1f), Header("Swipe Settings")]
        private float _swipeStrength;
        [SerializeField]
        private Vector2Direction _swipeDirection;

        //private void OnEnable()
        //{
        //    InputsManager.Inputs.Touches.Touch1.performed += CheckSwipe;
        //}

        //private void OnDisable()
        //{
        //    InputsManager.Inputs.Touches.Touch1.performed -= CheckSwipe;
        //}

        //private void CheckSwipe(InputAction.CallbackContext context)
        //{
        //    //if (!IsFingerInScreenLocation(InputsManager.Inputs.Touches.TouchDelta1.ReadValue<Vector2>()))
        //    //    return;

        //    Vector2 pos = InputsManager.Inputs.Touches.TouchPosition1.ReadValue<Vector2>();
        //    Vector2 deltaVector = InputsManager.Inputs.Touches.TouchDelta1.ReadValue<Vector2>();

        //    switch (_swipeDirection)
        //    {
        //        case Vector2Direction.Up:
        //            if (deltaVector.y < -_swipeStrength)
        //                OnInputPerformed?.Invoke();
        //            break;
        //        case Vector2Direction.Down:
        //            if (deltaVector.y > _swipeStrength)
        //                OnInputPerformed?.Invoke();
        //            break;
        //        case Vector2Direction.Left:
        //            if (deltaVector.x < -_swipeStrength)
        //                OnInputPerformed?.Invoke();
        //            break;
        //        case Vector2Direction.Right:
        //            if (deltaVector.x > _swipeStrength)
        //                OnInputPerformed?.Invoke();
        //            break;
        //    }
        //}
    }
}
