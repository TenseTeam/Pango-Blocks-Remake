namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlaceableBlock : BlockBase
    {
        private Vector2 _resetPosition;
        private Rigidbody2D _rb;

        public bool IsMoving => _rb.velocity.magnitude > 0.1f;
        public bool IsTilted => Mathf.Abs(transform.rotation.z) > 0.1f + Mathf.Abs(BlockData.zRotation);

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _rb);
        }

        public void SetResetPosition()
        {
            _resetPosition = transform.position;
        }

        public void ResetPosition()
        {
            DisableGravity();
            transform.rotation = Quaternion.Euler(Vector3.forward * BlockData.zRotation);
            transform.position = _resetPosition;
        }

        public void EnableCollider()
        {
            Collider.enabled = true;
        }

        public void DisableCollider()
        {
            Collider.enabled = false;
        }

        public void EnableGravity()
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }

        public void DisableGravity()
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}
