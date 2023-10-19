namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.ScriptableObjects;
    using ProjectPBR.Level.Blocks.Interfaces;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class PlaceableBlock : PooledBlock, ICastGameManager<GameManager>, IPlaceableBlock
    {
        private Vector2 _resetPosition;
        private Rigidbody2D _rb;
        private ContactFilter2D _contactFilter = new ContactFilter2D();

        public PlaceableBlockData Data { get; private set; }
        public PolygonCollider2D Collider { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsMoving => _rb.velocity.magnitude > 0.1f;
        public bool IsTilted => Mathf.Abs(transform.rotation.z) > 0.1f /* + Mathf.Abs(BlockData.zRotation)*/;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out PolygonCollider2D _collider);
            TryGetComponent(out _rb);

            Collider = _collider;
            _contactFilter.useLayerMask = true;
            _contactFilter.useTriggers = true;
            _contactFilter.layerMask = GameManager.GameGridManager.GridLayerMask;
        }

        public void Init(PlaceableBlockData data)
        {
            Data = data;
            Sprite.sprite = data.Sprite;
            Collider.points = data.ColliderData.Points;
            BlockType = data.BlockType;
        }

        public void SetResetPosition()
        {
            _resetPosition = transform.position;
        }

        public void ResetPosition()
        {
            DisableGravity();
            //transform.rotation = Quaternion.Euler(Vector3.forward * BlockData.zRotation);
            transform.rotation = Quaternion.identity;
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

        public bool IsInsideGrid() // TO DO: Use a method in Grid to convert a world position to a grid position.
        {
            Collider2D[] results = new Collider2D[1];
            return Physics2D.OverlapCollider(Collider, _contactFilter, results) > 0;
        }

        public override void Clear()
        {
            Data = null;
            Sprite.sprite = null;
            Collider.points = null;
            transform.rotation = Quaternion.identity;
        }
    }
}
