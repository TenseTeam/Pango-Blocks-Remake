namespace VUDK.Features.Main.CharacterController
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    [RequireComponent(typeof(Rigidbody))]
    public abstract class CharacterController : CharacterControllerBase
    {
        protected Rigidbody Rigidbody;

        public override bool IsGrounded => Physics.CheckSphere(transform.position + GroundedOffset, GroundedRadius, MainManager.Ins.GameStats.GroundLayerMask, QueryTriggerInteraction.Ignore);
        
        protected virtual void Awake()
        {
            TryGetComponent(out Rigidbody);
        }

        public override void StopCharacterOnPosition()
        {
            base.StopCharacterOnPosition();
            Rigidbody.velocity = Vector3.zero;
        }

        public override void StopCharacterOnXPosition()
        {
            base.StopCharacterOnXPosition();
            Rigidbody.velocity = new Vector3(0f, Rigidbody.velocity.y, Rigidbody.velocity.z);
        }

        public override void StopCharacterOnYPosition()
        {
            base.StopCharacterOnYPosition();
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
        }

        public virtual void StopCharacterOnZPosition()
        {
            StopCharacter();
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Rigidbody.velocity.y, 0f);
        }

        public override void Jump(Vector3 direction)
        {
            if (!CanJump) return;

            base.Jump(direction);
            Rigidbody.AddForce(direction * JumpForce, ForceMode.Impulse);
        }

        public override void MoveCharacter(Vector2 direction)
        {
            base.MoveCharacter(direction);

            Vector3 _movementDirection = transform.forward * InputMove.y + transform.right * InputMove.x;
            Vector3 velocityDirection = new Vector3(_movementDirection.x * CurrentSpeed, Rigidbody.velocity.y, _movementDirection.z * CurrentSpeed);
            Rigidbody.velocity = velocityDirection;
        }
    }
}
