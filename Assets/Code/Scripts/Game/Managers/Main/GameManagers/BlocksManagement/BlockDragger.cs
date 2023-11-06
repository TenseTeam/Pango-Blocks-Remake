namespace ProjectPBR.Managers.Main.GameManagers.BlocksManagement
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Features.Main.DragSystem;
    using ProjectPBR.Level.Blocks;

    public class BlockDragger : DraggerBase, ICastGameManager<GameManager>
    {
        public PlaceableBlockBase CurrentDraggedBlock { get; private set; }
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        protected override Vector3 CalculateTargetPosition()
        {
            return GameManager.MobileInputsManager.ScreenTouchPosition;
        }

        public void StartDrag(PlaceableBlockBase draggedBlock, Vector3 offset = default)
        {
            base.StartDrag(draggedBlock, offset);
            CurrentDraggedBlock = draggedBlock;
        }

        public override void StopDrag()
        {
            base.StopDrag();
            CurrentDraggedBlock = null;
        }
    }
}
