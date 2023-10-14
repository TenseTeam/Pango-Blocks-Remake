namespace ProjectPBR.Managers
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Player.PlayerHandler;
    using VUDK.Features.Main.InputSysten.MobileInputs;

    public class GameGridManager : MonoBehaviour, ICastGameManager<PBRGameManager>
    {
        [field: SerializeField, Header("Level Grid")]
        public LevelGrid Grid { get; private set; }

        [SerializeField, Header("Layer Masks")]
        private LayerMask _blocksLayerMask;

        [SerializeField, Header("Dragging Settings")]
        private float _followSpeed;

        private PlaceableBlock _currentDraggedBlock;
        private bool _isDragging;
        //private Vector3 _dragOffset;

        public PBRGameManager GameManager => MainManager.Ins.GameManager as PBRGameManager;
        private MobileInputsManager MobInputs => GameManager.MobileInputsManager;

        private void OnEnable()
        {
            InputsManager.Inputs.Interaction.Interact.canceled += TryToPlaceBlock;
        }

        private void OnDisable()
        {
            InputsManager.Inputs.Interaction.Interact.canceled -= TryToPlaceBlock;
        }

        private void Update()
        {
            if (InputsManager.Inputs.Interaction.Interact.IsInProgress())
            {
                if (TryGetBlockFromTouch(out PlaceableBlock block))
                {
                    //_dragOffset = MobInputs.ScreenTouchPosition - (Vector2)block.transform.position;
                    StartDrag(block);
                }
            }

            if (_isDragging)
                DragBlock();
        }

        public void StartDrag(PlaceableBlock block)
        {
            block.DisableCollider();
            _currentDraggedBlock = block;
            _isDragging = true;
        }

        private bool TryGetBlockFromTouch(out PlaceableBlock block)
        {
            if (_isDragging)
            {
                block = null;
                return false;
            }

            RaycastHit2D hit = MobInputs.Raycast2DFromTouch(_blocksLayerMask);

            if (hit && hit.transform.TryGetComponent(out PlaceableBlock bl))
            {
                block = bl;
                return true;
            }

            block = null;
            return false;
        }

        private void PlaceBlock(LevelTile tile)
        {
            _isDragging = false;
            _currentDraggedBlock.EnableCollider();
            tile.InsertBlock(_currentDraggedBlock);
        }

        private void TryToPlaceBlock(InputAction.CallbackContext context)
        {
            if (!_isDragging) return;

            if(MobInputs.IsTouchOn(out PlayerHandLayout layout))
            {
                layout.InsertInBounds(_currentDraggedBlock);
                ResetBlock(_currentDraggedBlock);
            }

            if(MobInputs.IsTouchOn(out LevelTile tile) && AreTilesFree(tile, _currentDraggedBlock))
                PlaceBlock(tile);
            else
                ResetBlock(_currentDraggedBlock);
        }

        private void ResetBlock(PlaceableBlock block)
        {
            _isDragging = false;
            block.EnableCollider();
            block.ResetPosition();
        }

        private void DragBlock()
        {
            _currentDraggedBlock.transform.position = Vector2.Lerp(_currentDraggedBlock.transform.position, GameManager.MobileInputsManager.ScreenTouchPosition, Time.deltaTime * _followSpeed);
        }

        private bool AreTilesFree(LevelTile fromTile, PlaceableBlock block)
        {
            LevelTile[,] tiles = Grid.GridTiles;

            for(int i = 0; i < block.BlockData.UnitLength; i++)
            {
                try
                {
                    if (tiles[fromTile.GridPosition.x + i, fromTile.GridPosition.y].IsOccupied) // Checks if the tiles are occupied
                        return false;
                }
                catch (IndexOutOfRangeException)
                {
                    return false; // Tile is out of range
                }
            }

            return true;
        }
    }
}
