namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlaceableBlock : BlockBase, ICastGameManager<GameManager>
    {
        private Vector2 _resetPosition;
        private Rigidbody2D _rb;
        private ContactFilter2D _contactFilter = new ContactFilter2D();

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsMoving => _rb.velocity.magnitude > 0.1f;
        public bool IsTilted => Mathf.Abs(transform.rotation.z) > 0.1f + Mathf.Abs(BlockData.zRotation);

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _rb);

            _contactFilter.useLayerMask = true;
            _contactFilter.useTriggers = true;
            _contactFilter.layerMask = GameManager.GridBlocksManager.GridLayerMask;
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

        public bool IsInsideGrid()
        {
            Collider2D[] results = new Collider2D[1];
            return Physics2D.OverlapCollider(Collider, _contactFilter, results) > 0;
        }

        [ContextMenu("Check")]
        private void Check()
        {
            Debug.Log(IsInsideGrid());
        }
    }
}
