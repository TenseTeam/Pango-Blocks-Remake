namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Serializable;
    using ProjectPBR.Level.Blocks.Interfaces;
    using ProjectPBR.Data.ScriptableObjects.Blocks;
    using ProjectPBR.Managers.Main.GameManagers;
    using ProjectPBR.GameConfig.Constants;

    public abstract class PlaceableBlockBase : DraggableBlockBase, ICastGameManager<GameManager>, IPlaceableBlock
    {
        private Rigidbody2D _rb;
        private bool _isInvalid;
        
        private Vector2 _resetPosition;
        private Vector2 _startPosition;
        private TimeDelay _resetTimer;

        protected BlockDataBase Data { get; private set; }

        public bool IsResettingPosition => _resetTimer.IsRunning;
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsMoving => _rb.velocity.magnitude > 0.1f;

        /// <summary>
        /// Checks if the block is invalid.
        /// </summary>
        /// <returns>True if it is invalid, False if not.</returns>
        public virtual bool IsInvalid() => _isInvalid; // I need to override this in ComplexPlaceableBlock

        protected virtual void Awake()
        {
            TryGetComponent(out _rb);
            _resetTimer = new TimeDelay();
        }

        /// <inheritdoc/>
        public virtual void Init(BlockDataBase data)
        {
            Data = data;
        }

        private void Update() => LerpPosition();

        /// <summary>
        /// Sets the reset position of the block to its current position.
        /// </summary>
        public void SetResetPosition()
        {
            _resetPosition = transform.position;
        }

        /// <summary>
        /// Sets the block IsInvalid as the given IsInvalid.
        /// </summary>
        /// <param name="isInvalid">Is Invalid to set.</param>
        public virtual void SetIsInvalid(bool isInvalid)
        {
            _isInvalid = isInvalid;
        }

        /// <summary>
        /// Starts the lerp resetting position.
        /// </summary>
        /// <param name="resetDuration">Duration of the lerp.</param>
        public void StartLerpResettingPosition(float resetDuration)
        {
            DisableGravity();
            transform.rotation = Quaternion.identity;
            _startPosition = transform.position;
            _resetTimer.ChangeDelay(resetDuration);
            _resetTimer.Start();
        }

        /// <summary>
        /// Enables the collider of the block.
        /// </summary>
        public abstract void EnableCollider();

        /// <summary>
        /// Disables the collider of the block.
        /// </summary>
        public abstract void DisableCollider();

        /// <summary>
        /// Enables the gravity of the block.
        /// </summary>
        public void EnableGravity()
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }

        /// <summary>
        /// Disables the gravity of the block.
        /// </summary>
        public void DisableGravity()
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        /// <summary>
        /// Checks if the block is tilted by its rotation.
        /// </summary>
        /// <returns>True if it is tilted, False if not.</returns>
        public bool IsTilted()
        {
            float maxTiltAngle = 1f;
            float zRotation = transform.rotation.eulerAngles.z;
            return zRotation > maxTiltAngle && zRotation < 360.0f - maxTiltAngle;
        }

        /// <inheritdoc/>
        public override void Clear()
        {
            Data = null;
            transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// Increases the render priority of the block.
        /// </summary>
        public abstract void IncreaseRender();

        /// <summary>
        /// Decreases the render priority of the block.
        /// </summary>
        public abstract void DecreaseRender();

        /// <inheritdoc/>
        public override void OnStartDragObject()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBlockStartDrag);
            transform.rotation = Quaternion.identity;
            IncreaseRender();
            DisableCollider();
        }

        /// <inheritdoc/>
        public void OnPlace()
        {
            EnableCollider();
            DecreaseRender();
        }

        /// <summary>
        /// Resets the block to its default values.
        /// </summary>
        public void PlaceableBlockReset()
        {
            IncreaseRender();
            DisableGravity();
            SetIsInvalid(false);
        }

        /// <summary>
        /// Lerps the position of the block to its reset position.
        /// </summary>
        private void LerpPosition()
        {
            if (!_resetTimer.Process()) return;
            transform.position = Vector2.Lerp(_startPosition, _resetPosition, _resetTimer.ClampNormalizedTime);

            if (Vector2.Distance(transform.position, _resetPosition) < 0.05f)
                OnBlockReturned();
        }

        /// <summary>
        /// On block returned to its reset position.
        /// </summary>
        private void OnBlockReturned()
        {
            transform.position = _resetPosition;
            EnableCollider();
            SetResetPosition();
            DecreaseRender();
            _resetTimer.Reset();
        }
    }
}