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
    using System;
    using static Unity.Collections.AllocatorManager;

    public abstract class PlaceableBlock : PooledBlock, ICastGameManager<GameManager>, IPlaceableBlock
    {
        private Rigidbody2D _rb;
        private bool _isInvalid;
        
        private Vector2 _resetPosition;
        private Vector2 _startPosition;
        private TimeDelay _resetTimer;

        public bool IsResettingPosition => _resetTimer.IsRunning;
        protected BlockData Data { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsMoving => _rb.velocity.magnitude > 0.1f;
        public virtual bool IsInvalid() => _isInvalid; // I need to override this in ComplexPlaceableBlock

        protected virtual void Awake()
        {
            TryGetComponent(out _rb);
            _resetTimer = new TimeDelay();
        }

        protected virtual void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<PlaceableBlock>(GameConstants.Events.OnBlockStartDrag, OnBlockBeingDragged);
            MainManager.Ins.EventManager.AddListener<PlaceableBlock>(GameConstants.Events.OnBlockPlaced, OnBlockPlaced);
            MainManager.Ins.EventManager.AddListener<PlaceableBlock>(GameConstants.Events.OnBlockStartReset, OnBlockStartReset);

        }

        protected virtual void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<PlaceableBlock>(GameConstants.Events.OnBlockStartDrag, OnBlockBeingDragged);
            MainManager.Ins.EventManager.RemoveListener<PlaceableBlock>(GameConstants.Events.OnBlockPlaced, OnBlockPlaced);
            MainManager.Ins.EventManager.RemoveListener<PlaceableBlock>(GameConstants.Events.OnBlockStartReset, OnBlockStartReset);
        }

        public virtual void Init(BlockData data)
        {
            Data = data;
        }

        private void Update() => LerpPosition();

        public void SetResetPosition()
        {
            _resetPosition = transform.position;
        }

        public virtual void SetIsInvalid(bool isInvalid)
        {
            _isInvalid = isInvalid;
        }

        public void StartLerpResettingPosition(float resetDuration)
        {
            DisableGravity();
            transform.rotation = Quaternion.identity;
            _startPosition = transform.position;
            _resetTimer.ChangeDelay(resetDuration);
            _resetTimer.Start();
        }

        public abstract void EnableCollider();

        public abstract void DisableCollider();

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

        public bool IsTilted()
        {
            float maxTiltAngle = 1f;
            float zRotation = transform.rotation.eulerAngles.z;
            return zRotation > maxTiltAngle && zRotation < 360.0f - maxTiltAngle;
        }

        public override void Clear()
        {
            Data = null;
            transform.rotation = Quaternion.identity;
        }

        public abstract void IncreaseRender();

        public abstract void DecreaseRender();

        private void LerpPosition()
        {
            if (!_resetTimer.Process()) return;
            transform.position = Vector2.Lerp(_startPosition, _resetPosition, _resetTimer.ClampNormalizedTime);

            if (Vector2.Distance(transform.position, _resetPosition) < 0.05f)
                BlockReturned();
        }

        private void BlockReturned()
        {
            transform.position = _resetPosition;
            EnableCollider();
            SetResetPosition();
            DecreaseRender();
            _resetTimer.Reset();
        }

        private void OnBlockBeingDragged(PlaceableBlock block)
        {
            if (block != this) return;

            transform.rotation = Quaternion.identity;
            IncreaseRender();
            DisableCollider();
        }

        private void OnBlockPlaced(PlaceableBlock block)
        {
            if (block != this) return;

            EnableCollider();
            DecreaseRender();
        }

        private void OnBlockStartReset(PlaceableBlock block)
        {
            if (block != this) return;

            IncreaseRender();
            DisableGravity();
            SetIsInvalid(false);
        }
    }
}