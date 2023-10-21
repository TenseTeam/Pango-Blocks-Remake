namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.ScriptableObjects;
    using ProjectPBR.Level.Blocks.Interfaces;

    //[RequireComponent(typeof(Rigidbody2D))]
    //[RequireComponent(typeof(Collider2D))]
    public abstract class PlaceableBlock : PooledBlock, ICastGameManager<GameManager>, IPlaceableBlock
    {
        private Vector2 _resetPosition;
        private Rigidbody2D _rb;

        public Collider2D Collider { get; protected set; }
        public BlockData Data { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsMoving => _rb.velocity.magnitude > 0.1f;
        public bool IsTilted => Mathf.Abs(transform.rotation.z) > 0.1f /* + Mathf.Abs(BlockData.zRotation)*/;

        protected virtual void Awake()
        {
            TryGetComponent(out _rb);
            TryGetComponent(out Collider2D collider);
            Collider = collider;
        }

        public abstract void Init(BlockData data);

        public void SetResetPosition()
        {
            _resetPosition = transform.position;
        }

        public void ResetPosition()
        {
            DisableGravity();
            transform.rotation = Quaternion.identity;
            transform.position = _resetPosition;
        }

        public abstract void EnableCollider();

        public abstract void DisableCollider();

        public abstract void IncreaseRender();

        public abstract void DecreaseRender();

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

        //public virtual bool IsInsideGrid() // TO DO: Use a method in Grid to convert a world position to a grid position OR use a trigger outside the grid.
        //{
        //    Collider2D[] results = new Collider2D[1];
        //    return Physics2D.OverlapCollider(Collider, _contactFilter, results) > 0;
        //}

        public override void Clear()
        {
            Data = null;
            transform.rotation = Quaternion.identity;
        }
    }
}
