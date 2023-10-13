namespace ProjectPBR.Managers
{
    using UnityEngine;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Player.PlayerHandler.Blocks;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using UnityEngine.InputSystem;
    using ProjectPBR.Level.Player.PlayerHandler;
    using static Unity.Collections.AllocatorManager;

    public class GridBlocksManager : MonoBehaviour, ICastGameManager<PBRGameManager>
    {
        [SerializeField, Header("Layer Masks")]
        private LayerMask _blocksLayerMask;

        [SerializeField, Header("Dragging Settings")]
        private float _followSpeed;

        private PlaceableBlock _currentDraggedBlock;
        private bool _isDragging;

        public PBRGameManager GameManager => MainManager.Ins.GameManager as PBRGameManager;

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
                    StartDrag(block);
                }
            }

            if (_isDragging)
                DragBlock();
        }

        public void StartDrag(PlaceableBlock block)
        {
            block.Collider.enabled = false;
            _currentDraggedBlock = block;
            block.transform.parent = null;
            _isDragging = true;
        }

        private bool TryGetBlockFromTouch(out PlaceableBlock block)
        {
            if (_isDragging)
            {
                block = null;
                return false;
            }

            RaycastHit2D hit = RaycastFromTouch(_blocksLayerMask);

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
            if (!tile.IsOccupied)
            {
                _currentDraggedBlock.Collider.enabled = true;
                _isDragging = false;
                tile.InsertBlock(_currentDraggedBlock);
            }
        }

        private void TryToPlaceBlock(InputAction.CallbackContext context)
        {
            if (!_isDragging) return;

            if (IsValidPosition(out LevelTile tile))
                PlaceBlock(tile);
            else
                ResetBlock(_currentDraggedBlock);
        }

        private void ResetBlock(PlaceableBlock block)
        {
            block.Collider.enabled = true;
            block.transform.position = block.OriginalPosition;
            //block.transform.parent = _playerHandBox.transform;
            _isDragging = false;
        }

        private bool IsValidPosition(out LevelTile tile)
        {
            if (IsTouchOnGridTile(out tile) /*&& !IsBlockMultipleOverlapping(tile)*/)
                return true;
            return false;
        }

        private bool IsTouchOnGridTile(out LevelTile tile)
        {
            RaycastHit2D hit = RaycastFromTouch(~0);
            if (hit)
            {
                Debug.Log(hit.transform.name);
                return hit.transform.TryGetComponent(out tile); // OR If is the player hand box
            }

            tile = null;
            return false;
        }

        private void DragBlock()
        {
            _currentDraggedBlock.transform.position = Vector2.Lerp(_currentDraggedBlock.transform.position, GameManager.MobileInputsManager.ScreenTouchPosition, Time.deltaTime * _followSpeed);
        }

        private RaycastHit2D RaycastFromTouch(LayerMask layerMask)
        {
            Vector2 origin = GameManager.MobileInputsManager.ScreenTouchPosition;
            Vector2 direction = Vector2.zero;
            float maxDistance = Mathf.Abs(MainManager.Ins.GameConfig.MainCamera.transform.position.z);
            return Physics2D.Raycast(origin, direction, maxDistance, layerMask);
        }
    }
}
