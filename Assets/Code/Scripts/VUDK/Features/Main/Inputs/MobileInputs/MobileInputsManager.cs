namespace VUDK.Features.Main.InputSysten.MobileInputs
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;
    using VUDK.Features.Main.Inputs.MobileInputs.Keys;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;

    [DefaultExecutionOrder(-500)]
    public sealed class MobileInputsManager : MonoBehaviour
    {
        [field: SerializeField, Header("Mobile Inputs")]
        public SerializableDictionary<MobileInputActionKeys, InputTouchBase> MobileInputsActions { get; private set; }

        /// <summary>
        /// Returns the position of the touch in the screen already converted with ScreenToWorldPoint.
        /// </summary>
        public Vector2 ScreenTouchPosition => MainManager.Ins.GameConfig.MainCamera.ScreenToWorldPoint(InputsManager.Inputs.Touches.TouchPosition0.ReadValue<Vector2>());
    }
}
