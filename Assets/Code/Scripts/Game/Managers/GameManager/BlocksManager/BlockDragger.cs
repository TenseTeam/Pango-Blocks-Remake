namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Level.Blocks;

    public class BlockDragger : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField]
        private float _followSpeed = 10f;

        //private Vector2 _dragOffset; TO DO: Implement this to drag the block from the point of touch of the block, not from the touch position.

        public PlaceableBlock CurrentDraggedBlock { get; private set; }
        public bool IsDragging => CurrentDraggedBlock != null;
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void Update()
        {
            if(IsDragging)
                DragBlock();
        }

        public void StartDrag(PlaceableBlock block)
        {
            CurrentDraggedBlock = block;
            block.transform.rotation = Quaternion.identity;
            block.IncreaseRender();
            block.DisableCollider();
        }

        public void StopDrag()
        {
            CurrentDraggedBlock.DecreaseRender();
            CurrentDraggedBlock = null;
        }
        
        private void DragBlock()
        {
            Vector2 targetPosition = GameManager.MobileInputsManager.ScreenTouchPosition;
            CurrentDraggedBlock.transform.position = Vector2.Lerp(CurrentDraggedBlock.transform.position, targetPosition, Time.deltaTime * _followSpeed);
        }
    }
}
