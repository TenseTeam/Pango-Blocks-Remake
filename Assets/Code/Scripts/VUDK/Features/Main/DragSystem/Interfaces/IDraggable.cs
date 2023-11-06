namespace VUDK.Features.Main.DragSystem
{
    using UnityEngine;

    public interface IDraggable
    {
        public void OnDragObject();
        public void OnEndDragObject();

        public Transform GetDragTransform();
    }
}