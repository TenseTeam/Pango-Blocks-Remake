namespace VUDK.Features.Main.InputSystem.MobileInputs
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
        public Vector2 ScreenTouchPosition => MainManager.Ins.GameStats.MainCamera.ScreenToWorldPoint(RawTouchPosition);
        public Vector2 RawTouchPosition => InputsManager.Inputs.Touches.TouchPosition0.ReadValue<Vector2>();

        /// <summary>
        /// Raycast2D from the finger touch position to the world.
        /// </summary>
        /// <param name="layerMask">Layers to ingnore.</param>
        /// <returns><see cref="RaycastHit2D"/> of the hit.</returns>
        public RaycastHit2D RaycastFromTouch2D(LayerMask layerMask)
        {
            Vector2 origin = ScreenTouchPosition;
            Vector2 direction = Vector2.zero;
            float maxDistance = Mathf.Abs(MainManager.Ins.GameStats.MainCamera.transform.position.z);
            return Physics2D.Raycast(origin, direction, maxDistance, layerMask);
        }

        /// <summary>
        /// Checks with <see cref="RaycastFromTouch2D(LayerMask)"/> if the touch is on a T Component.
        /// </summary>
        /// <typeparam name="T">T Component to check.</typeparam>
        /// <param name="component">Found T component.</param>
        /// <returns>True if the T component is found, False if not.</returns>
        public bool IsTouchOn2D<T>(out T component, LayerMask layerMask) where T : Component
        {
            RaycastHit2D hit = RaycastFromTouch2D(layerMask);
            if (hit)
            {
                return hit.transform.TryGetComponent(out component);
            }

            component = null;
            return false;
        }
    }
}
