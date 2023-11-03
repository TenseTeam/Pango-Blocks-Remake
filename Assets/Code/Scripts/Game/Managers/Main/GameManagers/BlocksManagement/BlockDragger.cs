﻿namespace ProjectPBR.Managers.Main.GameManagers.BlocksManagement
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.GameConfig.Constants;

    public class BlockDragger : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Drag Settings")]
        private float _followSpeed = 10f;

        private Vector3 _dragOffset;

        public PlaceableBlock CurrentDraggedBlock { get; private set; }
        public bool IsDragging => CurrentDraggedBlock != null;
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void Update()
        {
            if(IsDragging)
                DragBlock();
        }

        public void StartDrag(PlaceableBlock block, Vector2 blockDragOffset)
        {
            CurrentDraggedBlock = block;
            _dragOffset = blockDragOffset;
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBlockStartDrag, block);
        }

        public void StopDrag()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnBlockStopDrag, CurrentDraggedBlock);
            CurrentDraggedBlock = null;
        }
        
        private void DragBlock()
        {
            Vector2 fromPosition = CurrentDraggedBlock.transform.position;
            Vector2 targetPosition = GameManager.MobileInputsManager.ScreenTouchPosition - (Vector2)_dragOffset;
            CurrentDraggedBlock.transform.position = Vector2.Lerp(fromPosition, targetPosition, Time.deltaTime * _followSpeed);
        }
    }
}
