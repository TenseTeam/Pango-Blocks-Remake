namespace VUDK.Features.Main.Inputs.MobileInputs
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.InputSystem.MobileInputs;

    public abstract class TouchBehaviour : MonoBehaviour
    {
        protected MobileInputsManager MobileInputsManager;

        private bool _isTouchDown;

        protected abstract void Init(MobileInputsManager inputsManager);

        protected virtual void OnEnable()
        {
            InputsManager.Inputs.Touches.SingleTouch.canceled += TouchUp;
        }

        protected virtual void Update()
        {
            if(InputsManager.Inputs.Touches.SingleTouch.IsInProgress())
                TouchDown();
        }

        protected virtual void OnTouchDown2D()
        {
        }

        protected virtual void OnTouchUp2D()
        {
        }

        private void TouchDown()
        {
            if(_isTouchDown) return;
            _isTouchDown = true;

            RaycastHit2D hit = MobileInputsManager.RaycastFromTouch2D(1 << gameObject.layer);

            if (hit && hit.transform.TryGetComponent(out TouchBehaviour touch))
            {
                if (touch == this)
                    OnTouchDown2D();
            }
        }

        private void TouchUp(InputAction.CallbackContext obj)
        {
            if(_isTouchDown)
            {
                _isTouchDown = false;
                OnTouchUp2D();
            }
        }
    }
}
