namespace ProjectPBR.Level.Blocks
{
    using ProjectPBR.Level.Blocks.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.ScriptableObjects;
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Serializable;

    public abstract class PlaceableBlock : PooledBlock, ICastGameManager<GameManager>, IPlaceableBlock
    {
        private Rigidbody2D _rb;
        private bool _isInvalid;
        
        private Vector2 _resetPosition;
        private Vector2 _startPosition;
        private TimeDelay _resetTimer;

        public bool IsResettingPosition { get; private set; }
        protected BlockData Data { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsMoving => _rb.velocity.magnitude > 0.1f;

        protected virtual void Awake()
        {
            TryGetComponent(out _rb);
        }

        public virtual void Init(BlockData data)
        {
            Data = data;
        }

        private void Update()
        {
            if (IsResettingPosition)
                LerpPosition();
        }

        private void LerpPosition()
        {
            _resetTimer.AddDeltaTime();
            transform.position = Vector2.Lerp(_startPosition, _resetPosition, _resetTimer.ClampNormalizedTime);

            if (Vector2.Distance(transform.position, _resetPosition) < 0.05f)
            {
                transform.position = _resetPosition;
                SetResetPosition();
                IsResettingPosition = false;
                _resetTimer.Reset();
            }
        }

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
            IsResettingPosition = true;
            _resetTimer = new TimeDelay(resetDuration);
            _startPosition = transform.position;
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

        public virtual bool IsInvalid()
        {
            return _isInvalid;
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
    }
}