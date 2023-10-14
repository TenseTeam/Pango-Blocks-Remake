namespace VUDK.Features.Main.InputSysten.MobileInputs
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions;
    using VUDK.Features.Main.Inputs.MobileInputs.Keys;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Player.PlayerHandler;

    [DefaultExecutionOrder(-500)]
    public sealed class MobileInputsManager : MonoBehaviour
    {
        [field: SerializeField, Header("Mobile Inputs")]
        public SerializableDictionary<MobileInputActionKeys, InputTouchBase> MobileInputsActions { get; private set; }

        /// <summary>
        /// Returns the position of the touch in the screen already converted with ScreenToWorldPoint.
        /// </summary>
        public Vector2 ScreenTouchPosition => MainManager.Ins.GameConfig.MainCamera.ScreenToWorldPoint(InputsManager.Inputs.Touches.TouchPosition0.ReadValue<Vector2>());

        public RaycastHit2D Raycast2DFromTouch(LayerMask layerMask)
        {
            Vector2 origin = ScreenTouchPosition;
            Vector2 direction = Vector2.zero;
            float maxDistance = Mathf.Abs(MainManager.Ins.GameConfig.MainCamera.transform.position.z);
            return Physics2D.Raycast(origin, direction, maxDistance, layerMask);
        }

        public bool IsTouchOn<T>(out T component) where T : Component
        {
            RaycastHit2D hit = Raycast2DFromTouch(~0);
            if (hit)
                return hit.transform.TryGetComponent(out component);

            component = null;
            return false;
        }
    }
}
