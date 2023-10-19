namespace VUDK.Features.Main.CharacterController
{
    using System;
    using UnityEngine;

    public abstract class CharacterControllerBase : MonoBehaviour
    {
        [Header("Movement")]
        public float MoveSpeed;
        public float SprintSpeed;
        public float AirSpeed;
        public float JumpForce;

        [SerializeField, Header("Ground")]
        protected Vector3 GroundedOffset;
        //[SerializeField]
        //protected LayerMask GroundLayers;
        [SerializeField]
        protected float GroundedRadius;

        protected bool IsRunning;
        protected float CurrentSpeed;

        public Vector2 InputMove { get; private set; }
        public abstract bool IsGrounded { get; }
        protected bool CanJump => IsGrounded;

        public event Action<Vector2> OnMovement;
        public event Action OnStopMovement;
        public event Action OnJump;

        protected virtual void Update()
        {
            MoveUpdateVelocity();
        }

        /// <summary>
        /// Moves the character in the specified direction at the setted speed using rigidbody velocity.
        /// </summary>
        /// <param name="direction">Direction.</param>
        public virtual void MoveCharacter(Vector2 direction)
        {
            InputMove = direction;
            OnMovement?.Invoke(direction);
        }

        /// <summary>
        /// Makes the character jump in the specified direction at the setted jump force using rigidbody addforce.
        /// </summary>
        /// <param name="direction">Direction.</param>
        public virtual void Jump(Vector3 direction)
        {
            OnJump?.Invoke();
        }

        /// <summary>
        /// Stops the character movement.
        /// </summary>
        public void StopCharacter()
        {
            InputMove = Vector2.zero;
            StopSprint();
            OnStopMovement?.Invoke();
        }

        /// <summary>
        /// Immediately stops the character on its current position.
        /// </summary>
        public virtual void StopCharacterOnPosition()
        {
            StopCharacter();
        }

        public virtual void StopCharacterOnXPosition()
        {
            StopCharacter();
        }

        public virtual void StopCharacterOnYPosition()
        {
            StopCharacter();
        }

        public void Sprint()
        {
            IsRunning = true;
        }

        public void StopSprint()
        {
            IsRunning = false;
        }

        private void MoveUpdateVelocity()
        {
            CurrentSpeed = IsGrounded ? (IsRunning ? SprintSpeed : MoveSpeed) : AirSpeed;
        }

#if DEBUG
        protected virtual void OnDrawGizmos()
        {
            DrawCheckGroundSphere();
        }

        private void DrawCheckGroundSphere()
        {
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Gizmos.DrawSphere(transform.position + GroundedOffset, GroundedRadius);
        }
#endif
    }
}