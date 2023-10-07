namespace VUDK.Features.Main.Camera.CameraViews
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem;

    [RequireComponent(typeof(Camera))]
    public class CameraFreeLookBase : MonoBehaviour
    {
        [SerializeField, Header("Camera Settings")]
        protected float Sensitivity = 2f;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -90.0f;

        protected Vector2 LookRotation;

        private Camera _camera;

        protected virtual void Awake()
        {
            TryGetComponent(out _camera);
        }

        protected virtual void LateUpdate()
        {
            SetLookDirection();
            LookRotate();
        }

        private void SetLookDirection()
        {
            Vector2 _lookDirection = InputsManager.Inputs.Camera.Look.ReadValue<Vector2>();
            float mouseX = _lookDirection.x * Sensitivity;
            float mouseY = _lookDirection.y * Sensitivity;

            LookRotation.y += mouseX;
            LookRotation.x -= mouseY;
            LookRotation.x = Mathf.Clamp(LookRotation.x, BottomClamp, TopClamp);
        }

        protected virtual void LookRotate()
        {
            transform.localRotation = Quaternion.Euler(LookRotation.x, LookRotation.y, 0f);
        }
    }
}
