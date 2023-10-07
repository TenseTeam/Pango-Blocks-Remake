namespace VUDK.Features.Main.CharacterController
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public abstract class CharacterController : MonoBehaviour
    {
        [Header("Movement")]
        public float MoveSpeed;
        public float SprintSpeed;
        public float AirSpeed;
        public float JumpForce;

        [SerializeField, Header("Ground")]
        protected Vector3 GroundedOffset;
        [SerializeField]
        protected LayerMask GroundLayers;
        [SerializeField]
        private float _groundedRadius;

        protected bool IsRunning;

        private Vector3 _inputMove;
        private Rigidbody _rb;

        public bool IsGrounded => Physics.CheckSphere(transform.position + GroundedOffset, _groundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        protected bool CanJump => IsGrounded;

        private void Awake()
        {
            TryGetComponent(out _rb);
        }
        
        protected virtual void Update()
        {
            MoveUpdateVelocity();
        }

        /// <summary>
        /// Makes the character jump in the specified direction at a specified force using rigidbody addforce.
        /// </summary>
        /// <param name="direction">Direction.</param>
        /// <param name="force">Jump Force.</param>
        private void Jump(Vector3 direction, float force)
        {
            if (!CanJump)
                return;

            _rb.AddForce(direction * force, ForceMode.Impulse);
        }

        /// <summary>
        /// Moves the character in the specified direction at the setted speed using rigidbody velocity.
        /// </summary>
        /// <param name="direction">Direction.</param>
        protected virtual void MoveCharacter(Vector2 direction)
        {
            _inputMove = new Vector3(direction.x, 0, direction.y);
        }

        /// <summary>
        /// Makes the character jump in the specified direction at the setted jump force using rigidbody addforce.
        /// </summary>
        /// <param name="direction">Direction.</param>
        protected virtual void Jump(Vector3 direction)
        {
            Jump(direction, MoveSpeed);
        }

        /// <summary>
        /// Stops the character movement.
        /// </summary>
        protected void Stop()
        {
            _inputMove = Vector3.zero;
            StopSprint();
        }

        protected void Sprint()
        {
            IsRunning = true;
        }

        protected void StopSprint()
        {
            IsRunning = false;
        }

        private void MoveUpdateVelocity()
        {
            float _currentSpeed = IsGrounded ? (IsRunning ? SprintSpeed : MoveSpeed) : AirSpeed;

            Vector3 _movementDirection = transform.forward * _inputMove.z + transform.right * _inputMove.x;
            Vector3 velocityDirection = new Vector3(_movementDirection.x * _currentSpeed, _rb.velocity.y, _movementDirection.z * _currentSpeed);
            _rb.velocity = velocityDirection;
        }

#if DEBUG
        protected virtual void OnDrawGizmos()
        {
            DrawCheckGroundSphere();
        }

        private void DrawCheckGroundSphere()
        {
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Gizmos.DrawSphere(transform.position + GroundedOffset, _groundedRadius);
        }
#endif
    }
}